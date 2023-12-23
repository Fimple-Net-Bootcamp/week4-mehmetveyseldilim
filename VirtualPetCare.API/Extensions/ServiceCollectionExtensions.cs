
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VirtualPetCare.API.Validations;
using VirtualPetCare.Data;
using VirtualPetCare.Data.Contracts;
using VirtualPetCare.Data.DTOs;
using VirtualPetCare.Data.Entities;
using VirtualPetCare.Data.Entities.ConfigurationModels;
using VirtualPetCare.Data.Repositories;

namespace VirtualPetCare.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPostgresDbContext(this IServiceCollection services, IConfiguration configuration) 
        {
            string connectionString = configuration.GetConnectionString("Postgres")!;

            if(string.IsNullOrEmpty(connectionString)) 
            {
                Console.WriteLine("Connection string is null or empty");
                return services;
            }

            services.AddDbContext<VirtualPetCareDbContext>(options => 
            {
                options.UseNpgsql(connectionString, 
                b => b.MigrationsAssembly("VirtualPetCare.API")
                .MigrationsHistoryTable("__EFMigrationsHistory", schema: VirtualPetCareDbContext.SCHEMA_NAME));

            });

            return services;
        }

        public static IServiceCollection ConfigureRepositoryServices(this IServiceCollection services) 
        {
            services.AddScoped<ITypeRepository, TypeRepository>();

            services.AddScoped<IActivityRepository, ActivityRepository>();

            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();

            services.AddScoped<IFoodRepository, FoodRepository>();

            services.AddScoped<IHealthRepository, HealthRepository>();

            services.AddScoped<IPetRepository, PetRepository>();

            return services;

        }

        public static IServiceCollection AddAutoMapperService(this IServiceCollection services) 
        {
            // services.AddAutoMapper(typeof(Program));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, CustomIdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<VirtualPetCareDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfiguration = new JwtConfiguration();

            configuration.Bind(jwtConfiguration.Section, jwtConfiguration);

            var secretKey = Environment.GetEnvironmentVariable("SecretThree");

            Console.WriteLine($"Secret Key: {secretKey}");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfiguration.ValidIssuer,
                    ValidAudience = jwtConfiguration.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
                };
            });

            return services;
        }

        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<JwtConfiguration>
                (configuration.GetSection("JwtSettings")).AddOptions<JwtConfiguration>().ValidateDataAnnotations();

                return services;
        }

        public static IServiceCollection ConfigureValidations(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreatePetDTO>, CreatePetDTOValidator>();
            services.AddScoped<IValidator<UpdatePetDTO>, UpdatePetDTOValidator>();
            services.AddScoped<IValidator<UpdateHealthRecordDTO>, UpdateHealthRecordDTOValidator>();
            services.AddScoped<IValidator<CreateActivityDTO>, CreateActivityDTOValidator>();

            return services;
        }
    

    }
}
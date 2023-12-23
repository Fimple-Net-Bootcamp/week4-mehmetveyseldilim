using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VirtualPetCare.Data.Entities;

namespace VirtualPetCare.Data
{
    
    public class VirtualPetCareDbContext : IdentityDbContext<User, CustomIdentityRole, int,
    IdentityUserClaim<int>, ApplicationUserRole, IdentityUserLogin<int>,  
    IdentityRoleClaim<int>, IdentityUserToken<int> > 
    
    {
        public const string SCHEMA_NAME = "VirtualPetCareSchema";

        public DbSet<Pet> Pets {get; set;}

        public DbSet<Activity> Activities {get; set;}

        public DbSet<Food> Foods {get; set;}

        public DbSet<PetType> PetTypes {get; set;}

        public DbSet<PetTypeActivity> PetTypeActivities {get; set;}

        public DbSet<PetTypeFood> PetTypeFoods {get; set;}

        public DbSet<HealthRecord> HealthRecords {get; set;}

        public DbSet<PetFoodHistory> PetFoodHistories {get; set;}


        public VirtualPetCareDbContext(DbContextOptions<VirtualPetCareDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(SCHEMA_NAME);

            modelBuilder.Entity<User>(b =>  
            {     
                // Each User can have many entries in the UserRole join table  
                b.HasMany(e => e.UserRoles)  
                .WithOne(e => e.User)  
                .HasForeignKey(ur => ur.UserId)  
                .IsRequired();  
            });  

            modelBuilder.Entity<CustomIdentityRole>(b =>  
            {  
                // Each Role can have many entries in the UserRole join table  
                b.HasMany(e => e.UserRoles)  
                .WithOne(e => e.Role)  
                .HasForeignKey(ur => ur.RoleId)  
                .IsRequired();  
            });  



            //. Creating Composite Keys
            modelBuilder.Entity<PetTypeActivity>()
                .HasKey(pta => new { pta.ActivityId, pta.PetTypeId });

            modelBuilder.Entity<PetTypeFood>()
                .HasKey(pta => new { pta.FoodId, pta.PetTypeId });

            modelBuilder.HasPostgresEnum<PetTypeEnum>();
            modelBuilder.HasPostgresEnum<HealthStatus>();
            modelBuilder.Entity<PetType>().Property(x => x.PetTypeNameStringValue).HasConversion<string>();
            modelBuilder.Entity<HealthRecord>().Property(x => x.GeneralHealthStringValue).HasConversion<string>();
            

        }
    }
}
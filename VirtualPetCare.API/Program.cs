using VirtualPetCare.API.ActionFilters;
using VirtualPetCare.API.Extensions;
using VirtualPetCare.API.Helper;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureSerilog(builder.Configuration);
builder.Services.ConfigureValidations();
builder.Services.AddScoped<FluentValidationFilter>();
// Add services to the container.
builder.Services.AddPostgresDbContext(builder.Configuration);
builder.Services.ConfigureRepositoryServices();
builder.Services.AddAutoMapperService();
builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, MyJPIF.GetJsonPatchInputFormatter());
});
  

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);


var app = builder.Build();
app.UseCustomMeasureResponseTimeAsyncMiddleware();
app.UseCustomGlobalExceptionHandlerMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Seed(app.Environment);
}

app.UseHttpsRedirection();
app.UseCustomHttpLoggingMiddleware();
app.UseThrowUnauthorizedErrorMiddleware();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

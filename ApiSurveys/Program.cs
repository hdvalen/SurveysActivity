using System.Reflection;
using ApiSurveys.Extensions;
using Infrastructure;

using Microsoft.EntityFrameworkCore;
//Proporciona las clases de configuración de swagger
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

builder.Services.ConfigureCors();
builder.Services.AddControllers();
builder.Services.AddAplicacionServices();
builder.Services.AddCustomRateLimiter();

builder.Services.AddControllers();
// Add services to the container.

//builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();   
//Configuración incial de Swagger

builder.Services.AddSwaggerGen(c =>
{
   
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Project EF API",
        Version = "v1",
        Description = "API para el proyecto EF",
        Contact = new OpenApiContact
        {
            Name = "Grupo J1",
            Email = "hodethcaballero@gmail.com"
        }
    });
});

builder.Services.AddDbContext<SurveyContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
    options.UseNpgsql(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

var app = builder.Build();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();

    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {  
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project EF API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
app.UseRateLimiter();

app.UseAuthorization();
app.UseAuthentication();


app.Run();
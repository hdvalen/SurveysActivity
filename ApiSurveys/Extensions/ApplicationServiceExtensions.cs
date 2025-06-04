using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Repositories;

namespace ApiSurveys.Extensions;

public static class ApplicationServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()   //WithOrigins("https://dominio.com")
                .AllowAnyMethod()          //WithMethods("GET","POST")
                .AllowAnyHeader());        //WithHeaders("accept","content-type")
        });

    public static void AddAplicacionServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }
}
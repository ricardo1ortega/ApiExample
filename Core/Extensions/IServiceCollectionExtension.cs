using ApiExample.Core.Mappers;
using ApiExample.Db;
using ApiExample.Db.Context;
using ApiExample.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ApiExample.Core.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddOpenApiDocument(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Api Example Service",
                    Version = "v1",
                    Description = "A web API for management properties"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine("./", xmlFile);
                c.IncludeXmlComments(xmlFile);
            });

            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            services.AddSingleton(provider => {
                return new DbSettings
                {
                    Connection = Environment.GetEnvironmentVariable("DB_URL"),
                };
            });

            services.AddTransient<PropertyContext>();
            services.AddTransient<ServiceContext>();


            return services;
        }

        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddTransient<PropertyService>();

            return services;
        }

        public static IServiceCollection AddMappingProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(new[] {
                typeof(MappingProperty)
            });

            return services;
        }
    }
}

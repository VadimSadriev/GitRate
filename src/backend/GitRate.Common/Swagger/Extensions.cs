using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace GitRate.Common.Swagger
{
    public static class Extensions
    {
        public static IServiceCollection AddSwaggerDocs(this IServiceCollection services, IConfiguration configuration)
        {
            var swaggerSection = configuration.GetSection("swagger");

            var options = new SwaggerOptions();

            swaggerSection.Bind(options);

            if (!options.Enabled)
                return services;

            return services.AddSwaggerGen(x =>
             {
                 x.SwaggerDoc(options.Name, new OpenApiInfo { Title = options.Title, Version = options.Version });
                 x.DescribeAllParametersInCamelCase();

                 if (options.IncludeSecurity)
                 {
                     var openApiSecurityScheme = new OpenApiSecurityScheme
                     {
                         Description = "JWT Authorization using the bearer scheme",
                         Name = "Authorization",
                         In = ParameterLocation.Header,
                         Type = SecuritySchemeType.ApiKey,
                         Scheme = JwtBearerDefaults.AuthenticationScheme,
                         Reference = new OpenApiReference
                         {
                             Type = ReferenceType.SecurityScheme,
                             Id = JwtBearerDefaults.AuthenticationScheme
                         }
                     };

                     var security = new OpenApiSecurityRequirement();
                     security.Add(openApiSecurityScheme, new[] { "Bearer" });

                     x.AddSecurityDefinition("Bearer", openApiSecurityScheme);

                     x.AddSecurityRequirement(security);
                 }
             });
        }

        public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder builder, IConfiguration configuration)
        {
            var swaggerSection = configuration.GetSection("swagger");

            var options = new SwaggerOptions();

            swaggerSection.Bind(options);

            if (!options.Enabled)
                return builder;

            var routePrefix = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "swagger" : options.RoutePrefix;

            builder.UseSwagger(c => c.RouteTemplate = routePrefix + "/{documentName}/swagger.json");

            return builder.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/{routePrefix}/{options.Name}/swagger.json", options.Title);
                    c.RoutePrefix = routePrefix;
                });
        }
    }
}

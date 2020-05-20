﻿using GitRate.Common.Extensions;
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
            var swaggerSection = configuration.GetSection("Swagger").CheckExistence();

            var options = new SwaggerOptions();

            swaggerSection.Bind(options);

            if (!options.Enabled)
                return services;

            return services.AddSwaggerGen(swaggerGenOptions =>
            {
                swaggerGenOptions.SwaggerDoc(options.Name, new OpenApiInfo { Title = options.Title, Version = options.Version });
                swaggerGenOptions.DescribeAllParametersInCamelCase();

                if (!options.IncludeSecurity)
                    return;

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

                swaggerGenOptions.AddSecurityDefinition("Bearer", openApiSecurityScheme);

                swaggerGenOptions.AddSecurityRequirement(security);
            });
        }

        public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder builder, IConfiguration configuration)
        {
            var swaggerSection = configuration.GetSection("Swagger").CheckExistence();

            var options = new SwaggerOptions();

            swaggerSection.Bind(options);

            if (!options.Enabled)
                return builder;

            var routePrefix = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "swagger" : options.RoutePrefix;

            builder.UseSwagger(swaggerOptions => swaggerOptions.RouteTemplate = routePrefix + "/{documentName}/swagger.json");

            return builder.UseSwaggerUI(swaggerUIOptions =>
            {
                swaggerUIOptions.SwaggerEndpoint($"/{routePrefix}/{options.Name}/swagger.json", options.Title);
                swaggerUIOptions.RoutePrefix = routePrefix;
            });
        }
    }
}
using System.Reflection;
using Auth.Application.Behaviours;
using FluentValidation;
using GitRate.Common.Extensions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration, Assembly rootAssembly)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddValidatorsFromAssemblies(rootAssembly.GetApplicationAssemblies());
            
            return services;
        }
    }
}
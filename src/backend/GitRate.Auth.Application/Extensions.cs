using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            
            
            return services;
        }
    }
}
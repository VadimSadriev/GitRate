using GitRate.Common.Identity.Configuration;
using GitRate.Common.Identity.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GitRate.Common.Identity
{
    public static class Extensions
    {
        public static IServiceCollection AddCustomIdentity<TUser, TRole, TContext, TUserManagerService>(this IServiceCollection services, IConfiguration configuration)
            where TUser : IdentityUser
            where TRole : IdentityRole
            where TContext : DbContext
            where TUserManagerService : class, IUserManager
        {
            var identityConfigurationSection = configuration.GetSection("identityOptions");

            var identityConfiguration = new IdentityConfiguration();

            identityConfigurationSection.Bind(identityConfiguration);

            services.AddIdentity<TUser, TRole>(options =>
                {
                    options.Password.RequireDigit = identityConfiguration.RequireDigit;
                    options.Password.RequiredLength = identityConfiguration.RequiredLength;
                    options.Password.RequiredUniqueChars = identityConfiguration.RequiredUniqueChars;
                    options.Password.RequireLowercase = identityConfiguration.RequireLowercase;
                    options.Password.RequireNonAlphanumeric = identityConfiguration.RequireNonAlphanumeric;
                    options.Password.RequireUppercase = identityConfiguration.RequireUppercase;
                })
                .AddEntityFrameworkStores<TContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserManager, TUserManagerService>();

            return services;
        }
    }
}
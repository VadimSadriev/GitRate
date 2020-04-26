using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GitRate.Common.Database
{
    public static class Extensions
    {
        public static IServiceCollection AddDataContext<TContext>(this IServiceCollection services, IConfigurationSection dbSection)
            where TContext : DbContext
        {
            if (!dbSection.Exists())
                throw new ArgumentException("Missing db connection string");

            return services.AddDbContext<TContext>(x => { x.UseNpgsql(dbSection["connectionString"]); });
        }
    }
}
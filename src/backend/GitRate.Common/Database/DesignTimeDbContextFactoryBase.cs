using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GitRate.Common.Database
{
    public abstract class DesignTimeDbContextFactoryBase<TContext>
        where TContext : DbContext
    {
        protected abstract string BasePath { get;}
        private string AspNetCoreEnvironment = "Development";

        protected abstract TContext CreateContext(DbContextOptions<TContext> options);
        
        public TContext Create(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(BasePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{AspNetCoreEnvironment}.json", optional: true)
                .Build();

            var connectionString = configuration["database:connectionString"];
            
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("Connection string is empty");
            
            Console.WriteLine($"Creating {nameof(TContext)}. Connection string : {connectionString}");

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            optionsBuilder.UseNpgsql(connectionString);

            return CreateContext(optionsBuilder.Options);
        }
    }
}
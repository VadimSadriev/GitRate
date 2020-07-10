using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GitRate.Common.Database
{
    /// <summary> Design time factory for <see cref="DbContext"/> to manipulate with migrations, db during develop </summary>
    public abstract class DesignTimeDbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        /// <summary> Base path for asp net core settings </summary>
        protected abstract string BasePath { get;}

        /// <summary> Aspnet core environment </summary>
        private string AspNetCoreEnvironment = "Development";

        /// <summary> Implemented in concrete factory to create new <see cref="TContext"/> </summary>
        protected abstract TContext CreateContext(DbContextOptions<TContext> options);

        /// <summary> Creates new <see cref="TContext"/> </summary>
        public TContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(BasePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{AspNetCoreEnvironment}.json", optional: true)
                .Build();

            var connectionString = configuration["database:connectionString"];
            
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("Connection string is empty");
            
            Console.WriteLine($"Environment: {AspNetCoreEnvironment}");
            Console.WriteLine($"Creating {typeof(TContext).Name}. Connection string: {connectionString}");

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            optionsBuilder.UseNpgsql(connectionString);

            return CreateContext(optionsBuilder.Options);
        }
    }
}
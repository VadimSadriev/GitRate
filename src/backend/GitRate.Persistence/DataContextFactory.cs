using System.IO;
using GitRate.Common.Database;
using Microsoft.EntityFrameworkCore;

namespace GitRate.Persistence
{
    /// <summary>
    /// Factory for creating main user data context
    /// </summary>
    public class DataContextFactory : DesignTimeDbContextFactoryBase<DataContext> 
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        protected override string BasePath => $"{Directory.GetCurrentDirectory()}/../GitRate.Web.Api";
        
        protected override DataContext CreateContext(DbContextOptions<DataContext> options)
        {
            return new DataContext(options);
        }
    }
}
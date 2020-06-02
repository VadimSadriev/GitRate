using System.IO;
using GitRate.Common.Database;
using Microsoft.EntityFrameworkCore;

namespace GitRate.Persistence
{
    public class DataContextFactory : DesignTimeDbContextFactoryBase<DataContext> 
    {
        protected override string BasePath => $"{Directory.GetCurrentDirectory()}/../GitRate.Web.Api";
        
        protected override DataContext CreateContext(DbContextOptions<DataContext> options)
        {
            return new DataContext(options);
        }
    }
}
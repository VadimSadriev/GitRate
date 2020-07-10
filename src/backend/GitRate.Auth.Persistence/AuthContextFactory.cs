using System.IO;
using GitRate.Common.Database;
using Microsoft.EntityFrameworkCore;

namespace GitRate.Auth.Persistence
{
    /// <summary>
    /// Db context factory for <see cref="AuthContext"/>
    /// </summary>
    public class AuthContextFactory : DesignTimeDbContextFactoryBase<AuthContext>
    {
        protected override string BasePath => $"{Directory.GetCurrentDirectory()}/../GitRate.Auth.Web";
        
        protected override AuthContext CreateContext(DbContextOptions<AuthContext> options)
        {
            return new AuthContext(options);
        }
    }
}
using GitRate.Common.Database;
using GitRate.Common.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistence
{
    public class AuthContextFactory : DesignTimeDbContextFactoryBase<AuthContext>
    {
        protected override string BasePath => PathExtensions.GetExecutionDirectory();
        
        protected override AuthContext CreateContext(DbContextOptions<AuthContext> options)
        {
            return new AuthContext(options);
        }
    }
}
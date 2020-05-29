using System.Reflection;
using GitRate.Auth.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GitRate.Auth.Persistence
{
    /// <summary>
    /// Data context to manipulate with application users
    /// </summary>
    public class AuthContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Data context to manipulate with application users
        /// </summary>
        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }
        
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
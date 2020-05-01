using System.Reflection;
using Auth.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistence
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
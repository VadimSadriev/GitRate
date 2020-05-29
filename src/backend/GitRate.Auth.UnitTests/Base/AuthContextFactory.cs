using System;
using GitRate.Auth.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GitRate.Auth.UnitTests.Base
{
    public class AuthContextFactory
    {
        public static AuthContext Create()
        {
            var options = new DbContextOptionsBuilder<AuthContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            
            return new AuthContext(options);
        }

        public static void Destroy(AuthContext context)
        {
            context.Database.EnsureDeleted();
            
            context.Dispose();
        }
    }
}
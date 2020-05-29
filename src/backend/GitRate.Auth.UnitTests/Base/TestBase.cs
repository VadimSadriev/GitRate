using System;
using GitRate.Auth.Persistence;

namespace GitRate.Auth.UnitTests.Base
{
    public class TestBase : IDisposable
    {
        protected readonly AuthContext _context = AuthContextFactory.Create();

        public void Dispose()
        {
            AuthContextFactory.Destroy(_context);
        }
    }
}
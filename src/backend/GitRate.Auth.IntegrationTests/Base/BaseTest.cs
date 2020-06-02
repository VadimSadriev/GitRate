using System;
using System.Net.Http;
using System.Threading.Tasks;
using GitRate.Auth.Persistence;
using GitRate.Auth.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GitRate.Auth.IntegrationTests.Base
{
    public class BaseTest : IAsyncDisposable
    {
        private readonly string _dbName = Guid.NewGuid().ToString();
        private readonly IServiceProvider _serviceProvider;
        protected readonly HttpClient TestClient;

        public BaseTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll<DbContextOptions<AuthContext>>();
                        services.RemoveAll(typeof(AuthContext));
                        services.AddDbContext<AuthContext>(options => options.UseInMemoryDatabase(_dbName));
                    });
                });

            _serviceProvider = appFactory.Services;
            TestClient = appFactory.CreateClient();
            
            using var serviceScope = _serviceProvider.CreateScope();

            var context = serviceScope.ServiceProvider.GetService<AuthContext>();
            context.Database.EnsureCreated();
        }

        public async ValueTask DisposeAsync()
        {
            using var serviceScope = _serviceProvider.CreateScope();

            var context = serviceScope.ServiceProvider.GetService<AuthContext>();

            await context.Database.EnsureDeletedAsync();
        }
    }
}
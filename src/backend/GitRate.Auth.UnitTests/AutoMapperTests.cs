using AutoMapper;
using GitRate.Auth.Web.Controllers;
using GitRate.Common.Extensions;
using Xunit;

namespace GitRate.Auth.UnitTests
{
    public class AutoMapperTests
    {
        [Fact]
        public void Should_Be_Valid()
        {
            var assemblies = typeof(AccountController).Assembly.GetReferencedApplicationAssemblies();

            var mapperConfiguration = new MapperConfiguration(x => x.AddMaps(assemblies));

            mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}
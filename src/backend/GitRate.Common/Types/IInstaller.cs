using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GitRate.Common.Types
{
    /// <summary>
    /// Installs all required resources to application
    /// </summary>
    public interface IInstaller
    {
        void Install(IServiceCollection services, IConfiguration configuration);
    }
}
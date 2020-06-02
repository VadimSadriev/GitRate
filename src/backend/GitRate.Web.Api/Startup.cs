using System.Reflection;
using GitRate.Common.Authentication;
using GitRate.Common.Database;
using GitRate.Common.Logging;
using GitRate.Common.Mapping;
using GitRate.Common.MediatR;
using GitRate.Common.Mvc;
using GitRate.Common.Swagger;
using GitRate.Common.Time;
using GitRate.Persistence;
using GitRate.Web.Common.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GitRate.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var rootAssembly = Assembly.GetExecutingAssembly();
            
            services.AddCustomLogging(Configuration);
            services.AddCustomMvc(Configuration, rootAssembly);
            services.AddDataContext<DataContext>(Configuration);
            services.AddSwaggerDocs(Configuration);
            services.AddMediatR(rootAssembly);
            services.AddMappingProfiles(Assembly.GetExecutingAssembly());
            services.AddJwt(Configuration);
            services.AddSingleton<ITimeProvider, TimeProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseErrorMiddleware();

            app.UseSwaggerDocs(Configuration);
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
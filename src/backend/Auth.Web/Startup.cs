using System.Reflection;
using Auth.Persistence;
using GitRate.Common.Database;
using GitRate.Common.Logging;
using GitRate.Common.MediatR;
using GitRate.Common.Mvc;
using GitRate.Common.Swagger;
using GitRate.Web.Common.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Auth.Web
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
            services.AddCustomLogging(Configuration);
            services.AddCustomMvc(Configuration);
            services.AddDataContext<AuthContext>(Configuration);
            services.AddSwaggerDocs(Configuration);
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseErrorMiddleware();
            
            app.UseSwaggerDocs(Configuration);

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
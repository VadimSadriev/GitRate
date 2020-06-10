using System.Reflection;
using Auth.Application;
using Auth.Application.Services;
using GitRate.Auth.Domain;
using GitRate.Auth.Persistence;
using GitRate.Common.Authentication;
using GitRate.Common.Database;
using GitRate.Common.Identity;
using GitRate.Common.Logging;
using GitRate.Common.Mapping;
using GitRate.Common.MediatR;
using GitRate.Common.Mvc;
using GitRate.Common.Swagger;
using GitRate.Common.Time;
using GitRate.Web.Common.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GitRate.Auth.Web
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
            services.AddCustomMvc(Configuration, Assembly.GetExecutingAssembly());
            services.AddDataContext<AuthContext>(Configuration);
            services.AddSwaggerDocs(Configuration);
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddCustomIdentity<User, IdentityRole, AuthContext, UserManagerService>(Configuration);
            services.AddMappingProfiles(Assembly.GetExecutingAssembly());
            services.AddJwt(Configuration);
            services.AddSingleton<ITimeProvider, TimeProvider>();
            services.AddApplication(Configuration, Assembly.GetExecutingAssembly());
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

            app.UseErrorMiddleware();

            app.UseCors(builder =>
            {
                if (env.IsDevelopment())
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                }
            });

            app.UseSwaggerDocs(Configuration);

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
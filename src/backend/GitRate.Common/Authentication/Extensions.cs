using GitRate.Common.Extensions;
using GitRate.Common.Time;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GitRate.Common.Authentication
{
    /// <summary> Extensions for authentication </summary>
    public static class Extensions
    {
        /// <summary> Adds jwt authentication </summary>
        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection("Authentication:Jwt").CheckExistence();

            var jwtOptions = new JwtOptions();
            jwtSection.Bind(jwtOptions);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
            };

            services.AddTransient<IJwtService, JwtService>();
            services.Configure<JwtOptions>(jwtSection);
            services.AddSingleton(tokenValidationParameters);
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenValidationParameters;
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            //var body = new
                            //{
                            //    jwt = "token",
                            //    refresh_token = "refresh_token"
                            //};

                            //var ser = body.Serialize();
                            //await context.HttpContext.Response.WriteAsync(ser);
                            //context.HandleResponse();
                        },
                        OnAuthenticationFailed = async context =>
                        {
                          //  context.Principal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[0], "Bearer"));
                            //context.Success();
                        }
                    };
                });

            return services;
        }
    }
}
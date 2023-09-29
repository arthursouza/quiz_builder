using ApplicationCore.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;

namespace Api.Configuration;

public static class AuthConfigurationExtensions
{
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(jwtBearerOptions =>
        {
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration[JWTConfiguration.ValidIssuer],
                ValidAudience = configuration[JWTConfiguration.ValidAudience],
                IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[JWTConfiguration.Secret]))
            };

            jwtBearerOptions.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Response.Headers.Add("www-authenticate", "Bearer");
                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }
}

using Backoffice.API.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace BackOffice.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        string corsPolicy = configuration?["Cors:Policy"] ?? throw new ArgumentNullException("Cors:Policy", "Cors policy can't be null");
        string[] corsOrigins = configuration?["Cors:Origins"]?.Split(';') ?? throw new ArgumentNullException("Cors:Origins", "Cors origins must be separated by \';\' and can't be null.");
        string[] validIssuers = configuration?["TokenValidationParameters:ValidIssuers"]?.Split(';') ?? throw new ArgumentNullException("TokenValidationParameters:ValidIssuers", "Valid issuers must be separated by \';\' and can't be null.");
        string issuerSigningKey = configuration?["TokenValidationParameters:IssuerSigningKey"] ?? throw new ArgumentNullException("TokenValidationParameters:IssuerSigningKey", "IssuerSigningKey can't be null.");

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyWebApi", Version = "v1" });

            c.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                }
            );
            c.AddSecurityRequirement(
                new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = JwtBearerDefaults.AuthenticationScheme,
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                }
            );
        });

        services.AddTransient<IClaimsTransformation, ClaimsTransformer>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidIssuers = validIssuers,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthHelper.BuildRSAKey(issuerSigningKey),
                ValidateLifetime = true
            };
        });

        services.AddCors(options =>
        {
            options.AddPolicy(corsPolicy, builder =>
            {
                builder.WithOrigins(corsOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        return services;
    }
}
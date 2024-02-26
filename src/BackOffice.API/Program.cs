using Backoffice.API.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
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

builder.Services.AddTransient<IClaimsTransformation, ClaimsTransformer>();

builder.Services.AddAuthentication(options =>
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
        ValidIssuers = ["https://168.197.49.85:8443/realms/operators"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = AuthHelper.BuildRSAKey("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA4zq8ULwvTkQ5gk6yrN67bGW//ywW7d3GX1DtIdRrclbh9C1i/rMbzR5I6QXyuvBptePl45aOv583X3RIw9ySpP6g4abG5LEIYrRjvbPycKRgvV1CKHrGmwd7V9cAIbhs2Heg7gC1ziSLpoUuQCZJbQzy6AHGhOqkvav26n54ZYhmcUBwSYDLqEsvhv+VofBdw/whPtFWdmkIlo8lrxuIf1kvZJe9NU3JXxnZi+cQg56VepGLp+Hwd2+yZC+3FgKF2wor8byrfGtafNQqVdPIgutd+VmzIm0uuqj/+7QuV1aGisBxM5pvrQZ5MO2Ptih3B4tP1ndj/uU7yj9esbdN3wIDAQAB"),
        ValidateLifetime = true
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

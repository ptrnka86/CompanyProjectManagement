using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CompanyProjectManagement;
using Autofac.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(a =>
{
    a.RegisterModule(new AutofacRegistrationModule(builder.Configuration));

}
);
// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(GlobalExceptionFilter));
});
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("core", new OpenApiInfo
        {
            Title = "Core",
            Version = "coreV1",
            Description = "Core swagger doc"
        });

        c.SwaggerDoc("project", new OpenApiInfo
        {
            Title = "Project",
            Version = "projectV1",
            Description = "Project swagger doc"
        });

        c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                Password = new OpenApiOAuthFlow
                {
                    TokenUrl = new Uri("/api/login", UriKind.Relative),
                    Scopes = new Dictionary<string, string> { { "readAccess", "Access read operations" }, { "writeAccess", "Access write operations" } }
                }
            }
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement{{
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            }, new List<string>()
        }});
    });

string? secretKey = builder.Configuration.GetSection("TokenAuthentication:SecretKey").Value;
if (string.IsNullOrEmpty(secretKey))
{
    throw new ArgumentNullException("TokenAuthentication:SecretKey", "Secret key not found or empty.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("TokenAuthentication:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("TokenAuthentication:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/core/swagger.json", "Core API");
        c.SwaggerEndpoint("/swagger/project/swagger.json", "Project API");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();

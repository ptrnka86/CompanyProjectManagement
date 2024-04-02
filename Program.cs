using Autofac.Extensions.DependencyInjection;
using Autofac;
using Essity.Bp.Web;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(a =>
{
    a.RegisterModule(new AutofacRegistrationModule(builder.Configuration));

}
);
// Add services to the container.

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
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/core/swagger.json", "Core API");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();

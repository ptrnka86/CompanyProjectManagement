﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using CompanyProjectManagement.Data.Repository;
using CompanyProjectManagement.Security;
using CompanyProjectManagement.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CompanyProjectManagement;

public class AutofacRegistrationModule(IConfiguration configuration) : Module
{
    public IConfiguration Configuration { get; } = configuration;

    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterInstance(Configuration).As<IConfiguration>().SingleInstance();
        builder.RegisterType<UserProvider>().As<IUserProvider>().InstancePerLifetimeScope();
        builder.RegisterType<TokenGenerator>().As<ITokenGenerator>().InstancePerLifetimeScope();

        builder.RegisterType<ProjectRepository>().As<IProjectRepository>().InstancePerLifetimeScope();
        builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

        builder.RegisterType<ProjectService>().As<IProjectService>().InstancePerLifetimeScope();
    }


    public static IContainer CreateContainer(IServiceCollection services, IConfiguration configuration)
    {
        var builder = new ContainerBuilder();
        builder.Populate(services);
        builder.RegisterModule(new AutofacRegistrationModule(configuration));
        return builder.Build();
    }
}
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CompanyProjectManagement.Security;
using Microsoft.AspNetCore.Authentication;
using System.Data;

namespace Essity.Bp.Web;

public class AutofacRegistrationModule(IConfiguration configuration) : Module
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S3604:Member initializer values should not be redundant", Justification = "<Pending>")]
    public IConfiguration Configuration { get; } = configuration;

    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<UserProvider>().As<IUserProvider>().InstancePerLifetimeScope();
    }


    public static IContainer CreateContainer(IServiceCollection services, IConfiguration configuration)
    {
        var builder = new ContainerBuilder();
        builder.Populate(services);
        builder.RegisterModule(new AutofacRegistrationModule(configuration));
        return builder.Build();
    }
}
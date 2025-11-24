using Autofac;
using Autofac.Integration.WebApi;
using LeagueApi.Data;
using LeagueApi.Repositories;
using System.Reflection;
using System.Web.Http;

namespace LeagueApi.App_Start
{
    public static class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            // Register API controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register DbContext per request
            builder.RegisterType<AppDbContext>().InstancePerRequest();

            // Repositories - UnitOfWork
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}

using Microsoft.Practices.Unity;
using System.Web.Http;
using Data.Repositories;
using refactor_me.Infrastructure.Configuration;
using refactor_me.Services;
using Unity.WebApi;

namespace refactor_me
{
    public static class UnityConfiguration
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all the components

            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IProductRepository, ProductRepository>(new InjectionConstructor(StaticConfiguration.DatabaseConnectionString));

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
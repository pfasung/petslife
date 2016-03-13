using Microsoft.Practices.Unity;
using System.Web.Http;
using PetsLife.DAL;

namespace PetsLife
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            container.RegisterType<IPetsRepository, PetsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPetOwnersRepository, PetOwnersRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPetWalkersRepository, PetWalkersRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPetApprovalsRepository, PetApprovalsRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling =
                Newtonsoft.Json.PreserveReferencesHandling.Objects;
        }
    }
}

using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using E2E.App_Start;

namespace E2E
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            DependencyResolver.SetResolver(new NinjectResolver());
            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperConfiguration>());
        }
    }
}

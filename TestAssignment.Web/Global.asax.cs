using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TestAssignment.Web.IoC;
using TestAssignment.Web.Models;
using TestAssignment.DAL;

namespace TestAssignment.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            IoCConfig.Register();

            AutoMapper.Mapper.Initialize(cfg => 
            {
                cfg.CreateMap<EmployeeDataModel, Employee>();
                cfg.CreateMap<Employee, EmployeeDataModel>();
            });
        }
    }
}

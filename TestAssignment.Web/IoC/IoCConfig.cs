using System.Web;
using System.Web.Http;
using TestAssignment.Web.Models;
using TinyIoC;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TestAssignment.DAL;

namespace TestAssignment.Web.IoC
{
    public static class IoCConfig
    {
        public static void Register()
        {
            // Get IoC container
            var container = TinyIoCContainer.Current;

            container.Register<ApplicationDbContext>().AsPerRequestSingleton();
            container.Register<ApplicationSignInManager>().AsPerRequestSingleton();
            container.Register<ApplicationUserManager>().AsPerRequestSingleton();
            
            container.Register<IAuthenticationManager>((c, p) => HttpContext.Current.GetOwinContext().Authentication);
            container.Register<IUserStore<ApplicationUser>>((c, p) => 
                new UserStore<ApplicationUser>(container.Resolve<ApplicationDbContext>()));

            //Entity Framework
            container.Register<DAL.EF.EmployeeRepository>().AsPerRequestSingleton();
            container.Register<IRepository<Employee>>((c, p) => container.Resolve<DAL.EF.EmployeeRepository>());

            //Dapper
            //container.Register<IRepository<Employee>>(new DAL.Dapper.EmployeeRepository("DefaultConnection"));

            // Set MVC dep resolver
            System.Web.Mvc.DependencyResolver.SetResolver(new TinyIocMvcDependencyResolver(container));

            // Set Web API dep resolver
            GlobalConfiguration.Configuration.DependencyResolver = new TinyIocWebApiDependencyResolver(container);
        }
    }
}
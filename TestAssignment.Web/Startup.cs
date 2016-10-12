using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using TinyIoC;

[assembly: OwinStartupAttribute(typeof(TestAssignment.Web.Startup))]
namespace TestAssignment.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // Get IoC container
            var container = TinyIoCContainer.Current;
            container.Register<IDataProtectionProvider>(app.GetDataProtectionProvider());
        }
    }
}

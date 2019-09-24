using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(staffing.endpoints.Startup))]
namespace staffing.endpoints
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

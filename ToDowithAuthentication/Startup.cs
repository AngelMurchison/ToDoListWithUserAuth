using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToDowithAuthentication.Startup))]
namespace ToDowithAuthentication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

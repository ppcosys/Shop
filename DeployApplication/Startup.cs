using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DeployApplication.Startup))]
namespace DeployApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

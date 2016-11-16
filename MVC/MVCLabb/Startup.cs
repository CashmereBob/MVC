using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCLabb.Startup))]
namespace MVCLabb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

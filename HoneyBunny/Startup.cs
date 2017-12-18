using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HoneyBunny.Startup))]
namespace HoneyBunny
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

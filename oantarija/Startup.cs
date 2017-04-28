using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(oantarija.Startup))]
namespace oantarija
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

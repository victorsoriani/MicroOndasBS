using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MicroOndas.Startup))]
namespace MicroOndas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

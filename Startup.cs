using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ControleDeCasos.Startup))]
namespace ControleDeCasos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

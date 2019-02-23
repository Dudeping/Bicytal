using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bicytal.Startup))]
namespace Bicytal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

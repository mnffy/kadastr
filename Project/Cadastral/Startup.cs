using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cadastral.Startup))]
namespace Cadastral
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuthFail(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GestionNotasCunor.Startup))]
namespace GestionNotasCunor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(inzPJATKSNM.Startup))]
namespace inzPJATKSNM
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

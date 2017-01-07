using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(inzPJATKSNM.Startup))]
namespace inzPJATKSNM
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
            System.Threading.Timer t = new System.Threading.Timer(Callback, null, System.TimeSpan.Zero, System.TimeSpan.FromMinutes(5));
        }
        public static void Callback(object state)
        {
            inzPJATKSNM.Controllers.EditExistingSurveyController.updateSurveyStatusAndReturnVotes();
        }
    }
}

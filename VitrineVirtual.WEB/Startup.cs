using Microsoft.Owin;
using NLog;
using Owin;

[assembly: OwinStartupAttribute(typeof(VitrineVirtual.WEB.Startup))]
namespace VitrineVirtual.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            LogManager.LoadConfiguration("nlog.config");
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NoviKunstUitleen.Startup))]
namespace NoviKunstUitleen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

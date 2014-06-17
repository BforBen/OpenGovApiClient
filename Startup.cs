using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OpenGovApiClient.Startup))]
namespace OpenGovApiClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShowSubtitleInJwPlayer.Startup))]
namespace ShowSubtitleInJwPlayer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

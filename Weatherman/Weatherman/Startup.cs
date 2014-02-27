using Microsoft.Owin;
using Owin;
using WeathermanWeb;

[assembly: OwinStartup(typeof(Startup))]
namespace WeathermanWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WMarket.Startup))]
namespace WMarket
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

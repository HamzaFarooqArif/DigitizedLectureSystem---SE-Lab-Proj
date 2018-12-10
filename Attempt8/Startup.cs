using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Attempt8.Startup))]
namespace Attempt8
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PetsLife.Startup))]
namespace PetsLife
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

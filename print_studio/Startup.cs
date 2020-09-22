using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(print_studio.Startup))]
namespace print_studio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

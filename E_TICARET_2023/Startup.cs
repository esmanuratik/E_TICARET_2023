using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_TICARET_2023.Startup))]
namespace E_TICARET_2023
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

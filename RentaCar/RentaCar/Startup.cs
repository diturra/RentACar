using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RentaCar.Startup))]
namespace RentaCar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

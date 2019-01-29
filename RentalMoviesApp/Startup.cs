using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RentalMoviesApp.Startup))]
namespace RentalMoviesApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
           
        }
    }
}

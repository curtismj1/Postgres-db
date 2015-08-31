using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PostGres_Database.Startup))]
namespace PostGres_Database
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

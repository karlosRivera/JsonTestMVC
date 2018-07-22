using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JsonTestMVC.Startup))]
namespace JsonTestMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

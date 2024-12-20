using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(labelPrint.Startup))]
namespace labelPrint
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

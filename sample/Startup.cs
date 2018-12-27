using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sample.Startup))]
namespace sample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

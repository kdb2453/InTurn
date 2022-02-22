using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InTurn.Startup))]
namespace InTurn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

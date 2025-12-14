using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StickyNotes.Login.Startup))]
namespace StickyNotes.Login
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

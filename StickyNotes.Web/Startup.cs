using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StickyNotes.Web.Startup))]
namespace StickyNotes.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

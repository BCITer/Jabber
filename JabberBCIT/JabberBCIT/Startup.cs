using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JabberBCIT.Startup))]
namespace JabberBCIT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}

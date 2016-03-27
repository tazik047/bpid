using Microsoft.Owin;
using Owin;

namespace Lab1_des
{
    [assembly: OwinStartup(typeof(Lab1_des.Startup))]
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
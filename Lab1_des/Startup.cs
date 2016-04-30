using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Lab1_des.Startup))]
namespace Lab1_des
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
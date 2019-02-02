using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HabitBuilderApp.Startup))]
namespace HabitBuilderApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

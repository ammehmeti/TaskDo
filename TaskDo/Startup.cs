using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskDo.Startup))]
namespace TaskDo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

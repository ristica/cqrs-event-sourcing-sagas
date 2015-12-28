using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BankAccount.Client.Startup))]
namespace BankAccount.Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

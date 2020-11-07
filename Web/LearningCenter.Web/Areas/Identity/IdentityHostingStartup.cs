using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(LearningCenter.Web.Areas.Identity.IdentityHostingStartup))]
namespace LearningCenter.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
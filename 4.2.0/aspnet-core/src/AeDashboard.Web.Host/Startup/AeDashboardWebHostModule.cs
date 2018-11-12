using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AeDashboard.Configuration;

namespace AeDashboard.Web.Host.Startup
{
    [DependsOn(
       typeof(AeDashboardWebCoreModule))]
    public class AeDashboardWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public AeDashboardWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AeDashboardWebHostModule).GetAssembly());
        }
    }
}

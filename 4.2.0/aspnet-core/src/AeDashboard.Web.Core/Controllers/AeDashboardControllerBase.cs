using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace AeDashboard.Controllers
{
    public abstract class AeDashboardControllerBase: AbpController
    {
        protected AeDashboardControllerBase()
        {
            LocalizationSourceName = AeDashboardConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

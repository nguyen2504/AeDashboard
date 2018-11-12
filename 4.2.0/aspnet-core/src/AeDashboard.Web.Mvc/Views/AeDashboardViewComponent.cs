using Abp.AspNetCore.Mvc.ViewComponents;

namespace AeDashboard.Web.Views
{
    public abstract class AeDashboardViewComponent : AbpViewComponent
    {
        protected AeDashboardViewComponent()
        {
            LocalizationSourceName = AeDashboardConsts.LocalizationSourceName;
        }
    }
}

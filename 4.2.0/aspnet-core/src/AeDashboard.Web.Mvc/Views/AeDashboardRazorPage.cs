using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;

namespace AeDashboard.Web.Views
{
    public abstract class AeDashboardRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected AeDashboardRazorPage()
        {
            LocalizationSourceName = AeDashboardConsts.LocalizationSourceName;
        }
    }
}

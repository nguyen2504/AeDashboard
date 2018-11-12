using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using AeDashboard.Controllers;

namespace AeDashboard.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : AeDashboardControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}

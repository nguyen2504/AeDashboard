using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using AeDashboard.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace AeDashboard.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : AeDashboardControllerBase
    {
        //[AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Calendar");
        }
	}
}

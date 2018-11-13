using AeDashboard.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AeDashboard.Web.Controllers
{
    public class PersonalController : AeDashboardControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
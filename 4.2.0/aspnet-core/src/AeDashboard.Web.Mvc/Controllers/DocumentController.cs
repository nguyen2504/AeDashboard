using AeDashboard.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AeDashboard.Web.Controllers
{
    public class DocumentController : AeDashboardControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AeDashboard.Controllers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AeDashboard.Web.Controllers
{
    public class CalendarController : AeDashboardControllerBase
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}

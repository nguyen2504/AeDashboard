using Microsoft.AspNetCore.Antiforgery;
using AeDashboard.Controllers;

namespace AeDashboard.Web.Host.Controllers
{
    public class AntiForgeryController : AeDashboardControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}

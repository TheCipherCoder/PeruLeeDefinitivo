
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CPresentacion.Controllers
{

    public class DashboardController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "1")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

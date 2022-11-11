using Microsoft.AspNetCore.Mvc;

namespace FrontToBackFlowers.Areas.AdminPanel.Controllers
{
    public class DashboardController : BaseController
    {
      
        public IActionResult Index()
        {
            return View();
        }
    }
}

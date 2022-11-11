using FrontToBackFlowers.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FrontToBackFlowers.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult ErrorAction(int code)
        {
            ErrorViewModels error = new ErrorViewModels()
            {
                StatusCode = HttpContext.Response.StatusCode,
                Title = HttpContext.Response.Headers.ToString()

            };


            return View(error);
        }
    }
}

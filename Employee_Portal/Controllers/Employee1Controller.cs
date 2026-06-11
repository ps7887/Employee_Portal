using Microsoft.AspNetCore.Mvc;

namespace Employee_Portal.Controllers
{
    public class Employee1Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
    }
}

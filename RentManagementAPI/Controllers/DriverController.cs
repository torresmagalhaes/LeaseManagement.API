using Microsoft.AspNetCore.Mvc;

namespace RentManagementAPI.Controllers
{
    public class DriverController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

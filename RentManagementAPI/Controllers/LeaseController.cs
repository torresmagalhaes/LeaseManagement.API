using Microsoft.AspNetCore.Mvc;

namespace RentManagementAPI.Controllers
{
    public class LeaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

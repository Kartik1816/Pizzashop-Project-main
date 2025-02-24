using Microsoft.AspNetCore.Mvc;

namespace PizzaShop.Web.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

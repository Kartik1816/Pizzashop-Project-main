using Microsoft.AspNetCore.Mvc;

namespace PizzaShop.Web.Controllers;

public class ResetPasswordController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

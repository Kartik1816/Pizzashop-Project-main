using Microsoft.AspNetCore.Mvc;

namespace PizzaShop.Web.Controllers;

public class ForgotPasswordController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

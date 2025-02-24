using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PizzaShop.Web.Controllers;

public class UserListController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

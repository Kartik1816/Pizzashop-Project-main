using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.ViewModels;
using PIzzaShop.Service;
using PIzzaShop.Service.Interfaces;

namespace PizzaShop.Web.Controllers;

public class ChangePasswordController : Controller
{
    private readonly IGenerateJwt _generateJwt;
    private readonly IChangePasswordService _changePasswordService;
    public ChangePasswordController(IGenerateJwt generateJwt,IChangePasswordService changePasswordService)
    {
        _generateJwt=generateJwt;
        _changePasswordService=changePasswordService;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("/user/changepassword")]
    public IActionResult ChangePassword([FromBody] ChangePasswordViewModel model)
    {
        var userId=_generateJwt.GetUserIdFromJwtToken(Request.Cookies["token"]);
        var result = _changePasswordService.changePasswordService(model.OldPassword, model.NewPassword, userId);
        return new JsonResult (result);
    }
}

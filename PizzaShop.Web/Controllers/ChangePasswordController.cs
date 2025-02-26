using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.ViewModels;
using PIzzaShop.Service;
using PIzzaShop.Service.Interfaces;

namespace PizzaShop.Web.Controllers;

public class ChangePasswordController : Controller
{
    private readonly IGenerateJwt _generateJwt;
    private readonly IChangePasswordService _changePasswordService;

     private readonly IDashboardService _dashboardService;

    public ChangePasswordController(IGenerateJwt generateJwt,IChangePasswordService changePasswordService, IDashboardService dashboardService)
    {
        _generateJwt=generateJwt;
        _changePasswordService=changePasswordService;
        _dashboardService=dashboardService;
    }
    public async Task<IActionResult> IndexAsync()
    {
        var userId=_generateJwt.GetUserIdFromJwtToken(Request.Cookies["token"]);
        var username=await _dashboardService.getUsernameFromId(userId);
        var ImageUrL=await _dashboardService.getImageUrlFromId(userId);

        ChangePasswordViewModel changePasswordViewModel=new ChangePasswordViewModel
        {
            UserName=username,
            ProfileImageURL=ImageUrL
        };
        return View(changePasswordViewModel);
    }

    [HttpPost]
    [Route("/user/changepassword")]
    public  async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordViewModel model)
    {
        if(!ModelState.IsValid)
        {
            return new JsonResult(new{success=false,message="Validation error"});
        }
        var userId=_generateJwt.GetUserIdFromJwtToken(Request.Cookies["token"]);
       
        var result =await _changePasswordService.changePasswordService(model.OldPassword, model.NewPassword, userId);
        return new JsonResult (result);
    }
}

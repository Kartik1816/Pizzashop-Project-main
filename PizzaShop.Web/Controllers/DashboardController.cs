using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.ViewModels;
using PIzzaShop.Service;
using PIzzaShop.Service.Interfaces;

namespace PizzaShop.Web.Controllers;

public class DashboardController : Controller
{
    private readonly IGenerateJwt _generateJwt;

    private readonly IDashboardService _dashboardService;

    private readonly IRolePermissionService _rolePermissionService;

    public DashboardController(IGenerateJwt generateJwt, IDashboardService dashboardService,IRolePermissionService rolePermissionService)
    {
        _generateJwt=generateJwt;
        _dashboardService=dashboardService;
        _rolePermissionService=rolePermissionService;
    }
    public async Task<IActionResult> Index()
    {
        var userId=_generateJwt.GetUserIdFromJwtToken(Request.Cookies["token"]);
   
        var user=await _dashboardService.getUserFromId(userId);

                HttpContext.Session.SetString("ImageURL",user.ProfileImage);
                HttpContext.Session.SetString("UserName",user.Username);
                HttpContext.Session.SetInt32("RoleId", user.RoleId);
                List<RolePermissionModel> rolePermissionModels = await _rolePermissionService.getPermissionOfRole(user.RoleId);
                var permissionsBytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(rolePermissionModels);
                HttpContext.Session.Set("Permissions", permissionsBytes);

       
        return View();
    }
}

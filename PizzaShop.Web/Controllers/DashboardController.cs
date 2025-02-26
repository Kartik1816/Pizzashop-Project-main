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

    public DashboardController(IGenerateJwt generateJwt, IDashboardService dashboardService)
    {
        _generateJwt=generateJwt;
        _dashboardService=dashboardService;
    }
    public async Task<IActionResult> Index()
    {
        var userId=_generateJwt.GetUserIdFromJwtToken(Request.Cookies["token"]);
        var username=await _dashboardService.getUsernameFromId(userId);
        var ImageUrL=await _dashboardService.getImageUrlFromId(userId);

        DashboardViewModel dashboardViewModel=new DashboardViewModel
        {
            UserName=username,
            ProfileImageURL=ImageUrL
        };
        return View(dashboardViewModel);
    }
}

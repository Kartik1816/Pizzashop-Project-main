using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.Models;
using PizzaShop.Domain.ViewModels;
using PIzzaShop.Service;
using PIzzaShop.Service.Interfaces;

namespace PizzaShop.Web.Controllers;

public class RolesAndPermissionController : Controller
{
    private readonly IGenerateJwt _generateJwt;

    private readonly IDashboardService _dashboardService;

    private readonly IUserListService _userService;

    private readonly IRolePermissionService _rolePermissionService;

    public RolesAndPermissionController(IGenerateJwt generateJwt,IDashboardService dashboardService,IUserListService userService,IRolePermissionService rolePermissionService)
    {
        _generateJwt=generateJwt;
        _dashboardService=dashboardService;
        _userService=userService;
        _rolePermissionService=rolePermissionService;
    }
   
    public async Task<IActionResult> Index()
    {
        var userId= _generateJwt.GetUserIdFromJwtToken(Request.Cookies["token"]);
        var username=await _dashboardService.getUsernameFromId(userId);
        var imageUrl=await _dashboardService.getImageUrlFromId(userId);

        List<Role> roles=  _userService.getRoles();

        RolesAndPermissionViewModel rolesAndPermissionViewModel=new RolesAndPermissionViewModel
        {
            UserName=username,
            ProfileImageURL=imageUrl,
            Roles=roles
        };
        return View(rolesAndPermissionViewModel);
    }   

    [HttpGet]
    [Route("RolesAndPermission/Permission/{id}")]
    public async Task<IActionResult> Permission(int id)
    {
        var userId= _generateJwt.GetUserIdFromJwtToken(Request.Cookies["token"]);
        var username=await _dashboardService.getUsernameFromId(userId);
        var imageUrl=await _dashboardService.getImageUrlFromId(userId);
        var role=await _rolePermissionService.getRole(id);
        var permissions=await _rolePermissionService.getPermissions();
        var rolePermissions=await _rolePermissionService.getRolePermissions(id);
        PermissionViewModel permissionViewModel=new PermissionViewModel
        {
            UserName=username,
            ProfileImageURL=imageUrl,
            role=role,
            Permissions=permissions,
            RolePermissions=rolePermissions
        };
        return View(permissionViewModel);
    }

    [HttpPost]
    [Route("/RolesAndPermission/EditPermission")]
    public async Task<IActionResult> EditPermission([FromBody] List<SavePermissionModel> savePermissionModel)
    {
       return await _rolePermissionService.savePermissions(savePermissionModel);
    }
}

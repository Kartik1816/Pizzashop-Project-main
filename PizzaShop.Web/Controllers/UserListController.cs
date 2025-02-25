using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PizzaShop.Domain.ViewModels;
using PIzzaShop.Service;
using PIzzaShop.Service.Interfaces;

namespace PizzaShop.Web.Controllers;

public class UserListController : Controller
{
   private readonly IUserListService _userListService;
   private readonly IGenerateJwt _generateJwt;

   private readonly IEditProfileService _editProfileService;

    public UserListController(IUserListService userListService,IGenerateJwt generateJwt,IEditProfileService editProfileService)
    {
         _userListService=userListService;
         _generateJwt=generateJwt;
         _editProfileService=editProfileService;
    }
    public async Task<IActionResult> Index()
    {
        List<UserListViewModel> users=await _userListService.getUsers();
        ViewBag.model=users;
        return View();
    }


    [HttpGet]
    public async Task<JsonResult> SearchUsers(string searchTerm)
    { 
        var userListDetails =await _userListService.getSearchedUserListDetails(searchTerm);
        return Json(userListDetails);
    }

    [HttpGet]
    [Route("/UserList/EditUser/{id}")]
    public  async Task<IActionResult> EditUser(int id)
    {
        var userLoggedInId=_generateJwt.GetUserIdFromJwtToken(Request.Cookies["token"]);
          EditUserViewModel userViewModel =await _userListService.getUserDataFromUserId(id, userLoggedInId);
            var countries =_editProfileService.getCountries();
            var states = _editProfileService.getStates(userViewModel.CountryId);
            var cities = _editProfileService.getCities(userViewModel.StateId);
            var roles = _userListService.getRoles();
            
            ViewBag.Roles = roles;
            ViewBag.Countries = countries;
            ViewBag.States = states;
            ViewBag.Cities = cities;

      
         return View(userViewModel);
    }

    [HttpPut]
    [Route("/UserList/EditUser/{id}")]
    public  async Task<JsonResult> EditUser(EditUserViewModel userViewModel)
    {
        var userIdLoggedIn = _generateJwt.GetUserIdFromJwtToken(Request.Cookies["token"]);
        return await _userListService.editUserDataFromUserId(userIdLoggedIn,userViewModel);
    }
}

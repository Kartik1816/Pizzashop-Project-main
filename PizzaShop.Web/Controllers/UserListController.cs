using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PizzaShop.Domain.ViewModels;
using PIzzaShop.Service;
using PIzzaShop.Service.Interfaces;
using PIzzaShop.Service.Services;

namespace PizzaShop.Web.Controllers;

public class UserListController : Controller
{
   private readonly IUserListService _userListService;
   private readonly IGenerateJwt _generateJwt;

   private readonly IEditProfileService _editProfileService;

   private readonly EmailService _emailService;

   private readonly IDashboardService _dashboardService;

    public UserListController(IUserListService userListService,IGenerateJwt generateJwt,IEditProfileService editProfileService,EmailService emailService,IDashboardService dashboardService)
    {
         _userListService=userListService;
         _generateJwt=generateJwt;
         _editProfileService=editProfileService;
         _emailService=emailService;
         _dashboardService=dashboardService;
    }
    public async Task<IActionResult> Index()
    {
        var userId=_generateJwt.GetUserIdFromJwtToken(Request.Cookies["token"]);
        
        var username=await _dashboardService.getUsernameFromId(userId);
        var ImageUrL=await _dashboardService.getImageUrlFromId(userId);
        UserListViewModel userListViewModel=new UserListViewModel
        {
            UserName=username,
            ProfileImageURL=ImageUrL
        };
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

     public  async Task<IActionResult> AddUser()
    {
            var countries =_editProfileService.getCountries();
            var roles = _userListService.getRoles();
            var userLoggedInId = _generateJwt.GetUserIdFromJwtToken(Request.Cookies["token"]);
            var usernameLoggedIn =await _userListService.getUserNameFromId(userLoggedInId);
            var profileImageURL =await _userListService.getImageUrlFromId(userLoggedInId);
            var roleId =await _userListService.getRoleIdFromId(userLoggedInId);

           AddUserViewModel addUserViewModel = new AddUserViewModel
            {
                Username = usernameLoggedIn,
                ProfileImageURL = profileImageURL,
                RoleId = roleId
            };

            ViewBag.Roles = roles;
            ViewBag.Countries = countries;

            return View(addUserViewModel);

        
    }

        [HttpPost]
        [Route("UserList/AddUser")]
        public async Task<JsonResult> AddUser(AddUserViewModel addUserViewModel)
        {
            var userLoggedInId = _generateJwt.GetUserIdFromJwtToken(Request.Cookies["token"]);

            await _emailService.SendAddUserEmailAsync(addUserViewModel.Email, addUserViewModel.Password);

            return await _userListService.AddUser(userLoggedInId,addUserViewModel);
        }

        [HttpDelete]
        [Route("UserList/DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            return await _userListService.DeleteUser(userId); 

        }
}

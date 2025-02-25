using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.ViewModels;
using PIzzaShop.Service;
using PIzzaShop.Service.Interfaces;

namespace PizzaShop.Web.Controllers;

public class EditProfileController : Controller
{   private readonly IGenerateJwt _generateJwt;

    
    private readonly IEditProfileService _editProfileService;

   

    public EditProfileController(IGenerateJwt generateJwt,IEditProfileService editProfileService)
    {
        _generateJwt=generateJwt;
        _editProfileService=editProfileService;
    }
    public async Task<IActionResult> Index()
    {
         var id=_generateJwt.GetUserIdFromJwtToken(Request.Cookies["token"]);
         EditProfileViewModel editProfileViewModel=await _editProfileService.getCurrentUserViewModel(id);
         var country=_editProfileService.getCurrentUserCountry(id);
         var state=_editProfileService.getCurrentUserState(id);
         var city=_editProfileService.getCurrentUserCity(id);
         
         ViewBag.Country=country;
         ViewBag.State=state;
         ViewBag.City=city;
        return View(editProfileViewModel);
    }

    public  async Task<IActionResult> Edit()
    {
        var id=_generateJwt.GetUserIdFromJwtToken(Request.Cookies["token"]);
        var countryId= _editProfileService.getCountryIdByUserId(id);
        var stateId=_editProfileService.getStateIdByUserId(id);
        var Countries=_editProfileService.getCountries();
        var States= _editProfileService.getStates(countryId);
        var Cities=_editProfileService.getCities(stateId);
         
         EditProfileViewModel editProfileViewModel=await _editProfileService.getCurrentUserViewModel(id);

        ViewBag.Countries=Countries;
         ViewBag.States=States;
         ViewBag.Cities=Cities;
        return  View(editProfileViewModel);
    }

        [HttpGet]
        [Route("/Profile/GetStates")]
        public async Task<IActionResult> GetStates(int countryId)
        {
            var states = _editProfileService.getStates(countryId);
            return Json(states);
        } 

        [HttpGet]
        [Route("/Profile/GetCities")]
        public async Task<IActionResult> GetCities(int stateId)
        {
            var cities = _editProfileService.getCities(stateId);
            return Json(cities);
        }

        [HttpPost]
        [Route("/Profile/saveProfile")]
        public async Task<IActionResult> SaveChanges(EditProfileViewModel editProfileViewModel)
        {
            
            var id=_generateJwt.GetUserIdFromJwtToken(Request.Cookies["token"]);
            return await _editProfileService.saveProfile(editProfileViewModel,id);
        }
}

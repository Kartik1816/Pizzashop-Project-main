
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PizzaShop.Domain.DBContext;
using PizzaShop.Domain.Models;
using PizzaShop.Domain.ViewModels;
using PizzaShop.Repository.Interfaces;
using PIzzaShop.Service.Interfaces;

namespace PIzzaShop.Service.Services;

public class EditProfileService : IEditProfileService
{
    private readonly IEditProfileRepository _editProfileRepository;
    

    public EditProfileService(IEditProfileRepository editProfileRepository)
    {
        _editProfileRepository=editProfileRepository;
        
    }
    public List<Country> getCountries()
    {
        return  _editProfileRepository.getCountries();
    }

    public List<State> getStates(int countryId)
    {
        return  _editProfileRepository.getStates(countryId);
    }

    public List<City> getCities(int stateId)
    {
        return  _editProfileRepository.getCities(stateId);
    }


    public async Task<IActionResult> saveProfile( EditProfileViewModel editProfileViewModel,int userId)
    {
        return await _editProfileRepository.saveProfile(editProfileViewModel,userId);
    }

    public async Task<EditProfileViewModel> getCurrentUserViewModel(int id)
    {
        User user= await _editProfileRepository.getCurrentUserById(id);
       
        var roleName=  _editProfileRepository.getCurrentUserRoleById(user.RoleId);
        EditProfileViewModel editProfileViewModel=new EditProfileViewModel
        {
            FirstName=user.FirstName, 
            LastName =user.LastName,
            UserName =user.Username,
            Phone =user.Phone,
            CountryId =user.CountryId,
            StateId =user.StateId,
            CityId =user.CityId,
            Address =user.Address,
            ZipCode =user.ZipCode,
            Url =user.ProfileImage,
            Role =roleName,
            Email =user.Email
        };

        return editProfileViewModel;
    }

    public string getCurrentUserCountry(int id)
    {
         return  _editProfileRepository.getCurrentUserCountryById(id);
       
    }
     public string getCurrentUserState(int id)
    {
         
        return  _editProfileRepository.getCurrentUserStateById(id);
       
    }
     public string getCurrentUserCity(int id)
    {
        
        return  _editProfileRepository.getCurrentUserCityById(id);
    }

    public  int getCountryIdByUserId(int id)
    {
        return   _editProfileRepository.getCurrentUserCountryId(id);
    }

    public  int getStateIdByUserId(int id)
    {
        return  _editProfileRepository.getCurrentUserStateId(id);
    }
}

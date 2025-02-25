using PizzaShop.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.ViewModels;

namespace PIzzaShop.Service.Interfaces;

public interface IEditProfileService
{
    public List<Country> getCountries();

    public List<State> getStates(int countryId);

    public List<City> getCities(int stateId);

    public Task<IActionResult> saveProfile( EditProfileViewModel editProfileViewModel,int userId);

    public  Task<EditProfileViewModel> getCurrentUserViewModel(int id);

    public string getCurrentUserCountry(int id);

    public string getCurrentUserState(int id);
    public string getCurrentUserCity(int id);

    public  int getCountryIdByUserId(int id);

    public  int getStateIdByUserId(int id);

}

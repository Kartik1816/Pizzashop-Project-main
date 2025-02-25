using PizzaShop.Domain.Models;
using PizzaShop.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace PizzaShop.Repository.Interfaces;

public interface IEditProfileRepository
{
    public List<Country> getCountries();

    public List<State> getStates(int countryId);

    public List<City> getCities(int stateId);

    public Task<IActionResult> saveProfile( EditProfileViewModel editProfileViewModel,int userId);

    public Task<User> getCurrentUserById(int id);

    public  string getCurrentUserCountryById(int id);

    public  string getCurrentUserStateById(int id);

    public  string getCurrentUserCityById(int id);

    public  string getCurrentUserRoleById(int id);

    public int getCurrentUserCountryId(int id);

    public  int getCurrentUserStateId(int id);
}

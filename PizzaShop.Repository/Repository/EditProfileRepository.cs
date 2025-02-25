using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.DBContext;
using PizzaShop.Domain.Models;
using PizzaShop.Domain.ViewModels;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Repository;

public class EditProfileRepository : IEditProfileRepository
{
     private readonly PizzaShopDbContext _pizzaShopDbContext;
    

    public EditProfileRepository(PizzaShopDbContext pizzaShopDbContext)
    {
        _pizzaShopDbContext=pizzaShopDbContext;
        
    }
    public List<Country> getCountries()
    {
        return _pizzaShopDbContext.Countries.ToList();
    }

    public List<State> getStates(int countryId)
    {
        return  _pizzaShopDbContext.States.Where(s=>s.CountryId==countryId).ToList();
    }

    public List<City> getCities(int stateId)
    {
       
        return  _pizzaShopDbContext.Cities.Where(c=>c.StateId==stateId).ToList();
    }


    public async Task<IActionResult> saveProfile( EditProfileViewModel editProfileViewModel,int userId)
    {
        User? user=_pizzaShopDbContext.Users.FirstOrDefault(u=>u.Id==userId);
        
        if(user!=null  )
        {
            user.FirstName=editProfileViewModel.FirstName;
            user.LastName=editProfileViewModel.LastName;
            user.Username=editProfileViewModel.UserName;
            user.Phone=editProfileViewModel.Phone;
            user.CountryId=editProfileViewModel.CountryId;
            user.StateId=editProfileViewModel.StateId;
            user.CityId=editProfileViewModel.CityId;
            user.Address=editProfileViewModel.Address;
            user.ZipCode=editProfileViewModel.ZipCode;

             _pizzaShopDbContext.SaveChanges();
            return new JsonResult(new{success=true,message="Profile Updated successfully"});
        }
        else
        {
            return new JsonResult(new{success=false,message="User not found"});
        }
    }

    public async Task<User> getCurrentUserById(int id)
    {
        return  _pizzaShopDbContext.Users.FirstOrDefault(u=>u.Id==id);
         
    }

    public string getCurrentUserCountryById(int id)
    {
        User? user=_pizzaShopDbContext.Users.FirstOrDefault(u=>u.Id==id);
        return  _pizzaShopDbContext.Countries.FirstOrDefault(cn=>cn.Id==user.CountryId).Name;
    }

    public string getCurrentUserStateById(int id)
    {
        User? user=_pizzaShopDbContext.Users.FirstOrDefault(u=>u.Id==id);
        return  _pizzaShopDbContext.States.FirstOrDefault(cn=>cn.Id==user.StateId).Name;
    }

    public string getCurrentUserCityById(int id)
    {
        User? user=_pizzaShopDbContext.Users.FirstOrDefault(u=>u.Id==id);
        return  _pizzaShopDbContext.Cities.FirstOrDefault(cn=>cn.Id==user.CityId).Name;
    }
    public string getCurrentUserRoleById(int id)
    {
        return _pizzaShopDbContext.Roles.FirstOrDefault(r=>r.Id==id).Name;
    }

    public int getCurrentUserCountryId(int id)
    {
        User? user=_pizzaShopDbContext.Users.FirstOrDefault(u=>u.Id==id);
        return   _pizzaShopDbContext.Countries.FirstOrDefault(cn=>cn.Id==user.CountryId).Id;
    }

    public int getCurrentUserStateId(int id)
    {
        User? user=_pizzaShopDbContext.Users.FirstOrDefault(u=>u.Id==id);
        return  _pizzaShopDbContext.States.FirstOrDefault(cn=>cn.Id==user.StateId).Id;
    }
}

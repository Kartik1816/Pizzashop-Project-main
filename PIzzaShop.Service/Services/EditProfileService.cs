
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore.Metadata.Internal;
// using PizzaShop.Domain.DBContext;
// using PizzaShop.Domain.Models;
// using PizzaShop.Domain.ViewModels;
// using PIzzaShop.Service.Interfaces;

// namespace PIzzaShop.Service.Services;

// public class EditProfileService : IEditProfileService
// {
//     private readonly PizzaShopDbContext _pizzaShopDbContext;
    

//     public EditProfileService(PizzaShopDbContext pizzaShopDbContext)
//     {
//         _pizzaShopDbContext=pizzaShopDbContext;
        
//     }
//     public List<Country> getCountries()
//     {
//         return _pizzaShopDbContext.Countries.ToList();
//     }

//     public List<State> getStates(int countryId)
//     {
//         return _pizzaShopDbContext.States.Where(s=>s.CountryId==countryId).ToList();
//     }

//     public List<City> getCities(int stateId)
//     {
//         return _pizzaShopDbContext.Cities.Where(c=>c.StateId==stateId).ToList();
//     }


//     public IActionResult saveProfile( EditProfileViewModel editProfileViewModel,int userId)
//     {
//         User? user=_pizzaShopDbContext.Users.FirstOrDefault(u=>u.Id==userId);

        
//         if(user!=null  )
//         {
//             user.FirstName=editProfileViewModel.FirstName;
//             user.LastName=editProfileViewModel.LastName;
//             user.Username=editProfileViewModel.UserName;
//             user.Phone=editProfileViewModel.Phone;
//             user.CountryId=editProfileViewModel.CountryId;
//             user.StateId=editProfileViewModel.StateId;
//             user.CityId=editProfileViewModel.CityId;
//             user.Address=editProfileViewModel.Address;
//             user.ZipCode=editProfileViewModel.ZipCode;

//              _pizzaShopDbContext.SaveChanges();
//             return new JsonResult(new{success=true,message="Profile Updated successfully"});
//         }
//         else
//         {
//             return new JsonResult(new{success=false,message="User not found"});
//         }
//     }
// }

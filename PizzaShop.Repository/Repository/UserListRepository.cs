using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.DBContext;
using PizzaShop.Domain.Models;
using PizzaShop.Domain.ViewModels;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Repository;

public class UserListRepository : IUserListRepository
{
    private readonly PizzaShopDbContext _pizzaShopDbContext;
    private readonly IUserRepository _userRepository;

    public UserListRepository(PizzaShopDbContext pizzaShopDbContext,IUserRepository userRepository)
    {
        _pizzaShopDbContext=pizzaShopDbContext;
        _userRepository=userRepository;
    }
    public async Task<List<UserListViewModel>> getUserWithRole()
    {
       return  _pizzaShopDbContext.Users
       .Where(user=>user.IsDeleted == false)
        .Join(
            _pizzaShopDbContext.Roles,
            user => user.RoleId,
            role => role.Id,
            (user, role) => new UserListViewModel
            {
                UserId=user.Id,
                Username = user.Username,
                Email = user.Email,
                Phone = user.Phone,
                RoleName = role.Name,
                Status = user.Status
            })
        .ToList();
    }

    public async Task<JsonResult> saveEditedUser(int userLoggedInId, EditUserViewModel userViewModel)
    {
         var user =await _userRepository.getUserById(userViewModel.Id);
            if (user == null)
            {
                return new JsonResult(new { success = false, message = "User not found" });
            }
            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;
            user.Email = userViewModel.Email;
            user.Phone = userViewModel.PhoneNumber;
            user.Address = userViewModel.Address;
            user.ZipCode = userViewModel.ZipCode;
            user.Status = userViewModel.Status;
            user.RoleId = userViewModel.RoleIdRequestedUser;
            user.CountryId = userViewModel.CountryId;
            user.StateId = userViewModel.StateId;
            user.CityId = userViewModel.CityId;
            user.UpdatedBy = userLoggedInId;
            user.UpdatedAt = DateTime.Now;

            if (userViewModel.ProfileImage != null)
            {
                // store filename in db and save image in wwwroot/profile-images
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(userViewModel.ProfileImage.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profile-images", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    userViewModel.ProfileImage.CopyTo(fileStream);
                }
                user.ProfileImage = fileName;
            }
           _pizzaShopDbContext.SaveChanges();
            return new JsonResult(new { success = true, message = "User updated successfully" });
    }

     public  async Task<JsonResult> addUser(int userLoggedInId, AddUserViewModel addUserViewModel)
     {
        User user=new User
        {
            FirstName = addUserViewModel.FirstName,
            LastName = addUserViewModel.LastName,
            Email = addUserViewModel.Email,
            Phone = addUserViewModel.PhoneNumber,
            Address = addUserViewModel.Address,
            ZipCode = addUserViewModel.ZipCode,
            Password =BCrypt.Net.BCrypt.HashPassword(addUserViewModel.Password),
            Username= addUserViewModel.UsernameRequestedUser,
            RoleId = addUserViewModel.RoleIdRequestedUser,
            CountryId = addUserViewModel.CountryId,
            StateId = addUserViewModel.StateId,
            CityId = addUserViewModel.CityId,
            CreatedBy = userLoggedInId,
            UpdatedBy=userLoggedInId,
            UpdatedAt = DateTime.Now,
        };

        
            if (addUserViewModel.ProfileImage != null)
            {
                // store filename in db and save image in wwwroot/profile-images
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(addUserViewModel.ProfileImage.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profile-images", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    addUserViewModel.ProfileImage.CopyTo(fileStream);
                }
                user.ProfileImage = fileName;
            }

         _pizzaShopDbContext.Add(user);
         _pizzaShopDbContext.SaveChanges();

        return new JsonResult(new { success = true, message = "User Created successfully" });
     }

     public async Task<JsonResult> deleteUser(int userId)
     {
        var user = _pizzaShopDbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return new JsonResult(new { success = false, message = "User not found" });
            }
            user.IsDeleted = true;
            _pizzaShopDbContext.SaveChanges();
            return new JsonResult(new { success = true, message = "User deleted successfully" });
     }
}

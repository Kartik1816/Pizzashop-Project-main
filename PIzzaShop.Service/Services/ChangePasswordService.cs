using PizzaShop.Domain.Models;
using Microsoft.AspNetCore.Mvc;

using PIzzaShop.Service.Interfaces;
using PizzaShop.Domain.DBContext;
using PizzaShop.Repository.Interfaces;
using System.Threading.Tasks;

namespace PIzzaShop.Service.Services;

public class ChangePasswordService : IChangePasswordService
{
    private readonly PizzaShopDbContext _pizzaShopDbContext;
    private readonly IChangePasswordRepository _changePasswordRepository;

    public ChangePasswordService(PizzaShopDbContext pizzaShopDbContext,IChangePasswordRepository changePasswordRepository)
    {
        _pizzaShopDbContext=pizzaShopDbContext;
        _changePasswordRepository=changePasswordRepository;
    }
    public async Task<IActionResult> changePasswordService(string oldPassword,string newPassword,int userId)
    {
        // User? user=_pizzaShopContext.Users.FirstOrDefault(u=>u.Id==userId);
        // string pass=_pizzaShopContext.Users.FirstOrDefault(u=>u.Id==userId).Password;
        User user=await _changePasswordRepository.getUserById(userId);
        string pass= await _changePasswordRepository.getPasswordByUser(userId);
        if(user!=null)
        {
            if(BCrypt.Net.BCrypt.Verify(oldPassword,pass))
            {
                user.Password=BCrypt.Net.BCrypt.HashPassword(newPassword);
                _pizzaShopDbContext.SaveChanges();
                return new JsonResult(new{success=true,message="Password Changed successfully!"});
            }
            else{
                return new JsonResult(new{success=false,message="Current password does not match"});
            }
        }
        else
        {
            return new JsonResult(new{success=false,message="user not found"});
        }

    }
}

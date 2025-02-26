using Microsoft.EntityFrameworkCore;
using PizzaShop.Domain.Models;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Domain.DBContext;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace PizzaShop.Repository.Repository;

public class UserRepository : IUserRepository
{
    private readonly PizzaShopDbContext _pizzaShopDbContext;

    public UserRepository(PizzaShopDbContext pizzaShopDbContext)
    {
        _pizzaShopDbContext=pizzaShopDbContext;
    }
    public async Task<User> getUserByEmailAndPasswordAsync(string email, string password)
        {
            var user = await _pizzaShopDbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return user;
            }

            return null!;
        }

        public async Task<string> getRoleNameFromRoleId(int id)
        {
            return _pizzaShopDbContext.Roles.FirstOrDefault(r=>r.Id==id).Name;
        }

        public async Task<string> getUserNameFromUserId(int id)
        {
            return _pizzaShopDbContext.Users.FirstOrDefault(u=>u.Id == id).Username;
        }

        public async Task<string> getImageUrlFromUserId(int id)
        {
            return _pizzaShopDbContext.Users.FirstOrDefault(u=>u.Id == id).ProfileImage;
        }

         public async Task<User> getUserById(int id)
         {
            return _pizzaShopDbContext.Users.FirstOrDefault(u=>u.Id == id);
         }
         public async Task<int> getRoleIdFromUserId(int id)
         {
            return _pizzaShopDbContext.Users.FirstOrDefault(u=>u.Id == id).RoleId;
         }

         public  List<Role> getRoleList()
         {
            return _pizzaShopDbContext.Roles.ToList();
         }
         
}



using Microsoft.EntityFrameworkCore;
using PizzaShop.Domain.Models;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Domain.DBContext;

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
            return await _pizzaShopDbContext.Users
                .Where(u => u.Email == email && u.Password == password)
                .FirstOrDefaultAsync();
        }

        public async Task<string> getRoleNameFromRoleId(int id)
        {
            return _pizzaShopDbContext.Roles.FirstOrDefault(r=>r.Id==id).Name;
        }
}


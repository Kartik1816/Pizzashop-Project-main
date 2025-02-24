using PizzaShop.Domain.DBContext;
using PizzaShop.Domain.Models;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Repository;

public class ChangePasswordRepository : IChangePasswordRepository
{
    private readonly PizzaShopDbContext _pizzaShopDbContext;
    public ChangePasswordRepository(PizzaShopDbContext pizzaShopDbContext)
    {
        _pizzaShopDbContext=pizzaShopDbContext;
    }
     public async Task<User> getUserById(int id)
     {
          return _pizzaShopDbContext.Users.FirstOrDefault(u=>u.Id==id);
         
     }

    public async Task<string> getPasswordByUser(int id)
    {
        return _pizzaShopDbContext.Users.FirstOrDefault(u=>u.Id==id).Password;
    }
}

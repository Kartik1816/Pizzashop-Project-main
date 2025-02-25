using PizzaShop.Domain.DBContext;
using PizzaShop.Domain.Models;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Repository;

public class ForgotPasswordRepository : IForgotPasswordRepository
{
    private readonly PizzaShopDbContext _pizzaShopDbContext;

    public ForgotPasswordRepository(PizzaShopDbContext pizzaShopDbContext)
    {
        _pizzaShopDbContext=pizzaShopDbContext;
    }
    public async Task<User>getUserFromEmail(string email)
    {
        return _pizzaShopDbContext.Users.FirstOrDefault(u=>u.Email==email);
    }
}

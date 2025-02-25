using PizzaShop.Domain.Models;

namespace PizzaShop.Repository.Interfaces;

public interface IForgotPasswordRepository
{
    public Task<User>getUserFromEmail(string email);
}

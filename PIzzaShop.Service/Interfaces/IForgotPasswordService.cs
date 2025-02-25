using PizzaShop.Domain.Models;

namespace PIzzaShop.Service.Interfaces;

public interface IForgotPasswordService
{
    public Task<User> getUserFromEmail(string email);
}

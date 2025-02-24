using PizzaShop.Domain.Models;

namespace PizzaShop.Repository.Interfaces;

public interface IChangePasswordRepository
{
    public Task<User> getUserById(int id);

    public Task<string> getPasswordByUser(int id);
}

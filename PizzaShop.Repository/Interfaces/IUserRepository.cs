
using PizzaShop.Domain.Models;


namespace PizzaShop.Repository.Interfaces;

public interface IUserRepository
{
    public  Task<User> getUserByEmailAndPasswordAsync(string email,string password);

    public Task<string> getRoleNameFromRoleId(int id);
}

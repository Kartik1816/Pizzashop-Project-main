
using PizzaShop.Domain.Models;


namespace PizzaShop.Repository.Interfaces;

public interface IUserRepository
{
    public  Task<User> getUserByEmailAndPasswordAsync(string email,string password);

    public Task<string> getRoleNameFromRoleId(int id);

    public  Task<string> getUserNameFromUserId(int id);

    public  Task<string> getImageUrlFromUserId(int id);

    public Task<User> getUserById(int id);

    public Task<int> getRoleIdFromUserId(int id);

    public  List<Role> getRoleList();
}

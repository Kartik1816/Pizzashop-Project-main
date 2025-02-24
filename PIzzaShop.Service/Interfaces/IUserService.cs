
using PizzaShop.Domain.Models;

namespace PIzzaShop.Service.Interfaces;

public interface IUserService
{
      public Task<User> validateUserAsync(string email,string password);

      public Task<string> getRoleName(int id);
}

using PizzaShop.Domain.Models;

namespace PIzzaShop.Service;

public interface IGenerateJwt
{
    string GenerateJwtToken(User user,string role);
    int GetUserIdFromJwtToken(string token);
    
}

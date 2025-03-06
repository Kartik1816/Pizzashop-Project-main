using PizzaShop.Domain.Models;

namespace PIzzaShop.Service.Interfaces;

public interface IDashboardService
{
    public Task<User> getUserFromId(int userId);
    public Task<string> getImageUrlFromId(int userId);

    public  Task<string> getUsernameFromId(int userId);
}

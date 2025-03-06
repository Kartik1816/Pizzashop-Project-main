
using System.Threading.Tasks;
using PizzaShop.Domain.DBContext;
using PizzaShop.Domain.Models;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Repository.Repository;
using PIzzaShop.Service.Interfaces;

namespace PIzzaShop.Service.Services;

public class DashboardService : IDashboardService
{
   
    private readonly IUserRepository _userRepository;
    public DashboardService(IUserRepository userRepository)
    {
        _userRepository=userRepository;
    }

    public async Task<string> getUsernameFromId(int userId)
    {
       return await _userRepository.getUserNameFromUserId(userId);
    }

    public async Task<User> getUserFromId(int userId)
    {
       return await _userRepository.getUserById(userId);
    }
    public async Task<string> getImageUrlFromId(int userId)
    {
        return await _userRepository.getImageUrlFromUserId(userId);
    }
}

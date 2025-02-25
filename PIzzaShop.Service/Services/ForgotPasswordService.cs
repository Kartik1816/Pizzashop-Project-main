using PizzaShop.Domain.Models;
using PizzaShop.Repository.Interfaces;
using PIzzaShop.Service.Interfaces;

namespace PIzzaShop.Service.Services;

public class ForgotPasswordService : IForgotPasswordService
{
    private readonly IForgotPasswordRepository _forgotPasswordRepository;
    
    public ForgotPasswordService(IForgotPasswordRepository forgotPasswordRepository)
    {
        _forgotPasswordRepository=forgotPasswordRepository;
    }
    public async Task<User> getUserFromEmail(string email)
    {
        return await  _forgotPasswordRepository.getUserFromEmail(email);
    }
}

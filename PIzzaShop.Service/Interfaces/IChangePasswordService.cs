using Microsoft.AspNetCore.Mvc;

namespace PIzzaShop.Service.Interfaces;

public interface IChangePasswordService
{
    public Task<IActionResult> changePasswordService(string oldPassword,string newPassword,int userId);
}

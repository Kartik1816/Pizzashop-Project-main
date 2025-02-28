using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.Models;
using PizzaShop.Domain.ViewModels;
using PIzzaShop.Service;
using PIzzaShop.Service.Interfaces;

namespace PizzaShop.Web.Controllers;

public class AuthController : Controller
{
    private readonly IUserService _userService;

    private readonly IGenerateJwt _generateJwt;
    public AuthController(IUserService userService,IGenerateJwt generateJwt)
    {
        _userService=userService;
        _generateJwt=generateJwt;
    }
    public IActionResult Index()
    {
        return View();
    }
     [HttpPost]
    [Route("/api/validate")]
    public async Task<IActionResult> Login([FromBody] UserViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return Ok(new { success = false, message = "Login failed" });
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Validation failed", errors = ModelState });
            }

            var user = await _userService.validateUserAsync(model.Email, model.Password);

            if (user!=null)
            {
                
                if(user.IsDeleted==true)
                {
                    return Ok(new { success = false, message = "User is deleted" });
                }   
                if(user.Status==false)
                {
                    return Ok(new { success = false, message = "User is not active" });
                }

                
                var roleid=user.RoleId;
                var role = await _userService.getRoleName(roleid);
                var token = _generateJwt.GenerateJwtToken(user, role);

                if(user.HasLoggedInBefore==false)
                {
                 return Ok(new { success = true, message = "Reset Your Password First Time to Continue",token=token,hasLoggedInBefore=false});
                }
                return Ok(new { success = true, message = "Login success",token=token,hasLoggedInBefore=true});
                
            }
            else
            {
                return Ok(new { success = false, message = "Login failed" });
            }
        }

}

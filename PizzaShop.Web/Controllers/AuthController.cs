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
                

                var roleid=user.RoleId;
                var role = await _userService.getRoleName(roleid);
                var token = _generateJwt.GenerateJwtToken(user, role);
                // var Username=_pizzaShopContext.Users.FirstOrDefault(u=>u.Email==model.Email).Username;
                // var ProfileImage=_pizzaShopContext.Users.FirstOrDefault(u=>u.Email==model.Email).ProfileImage;
                // HttpContext.Session.SetString("UserName", Username);
                // HttpContext.Session.SetString("ImageURL", ProfileImage);
                return Ok(new { success = true, message = "Login success",token=token});
                
            }
            else
            {
                return Ok(new { success = false, message = "Login failed" });
            }
        }

}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Domain.DBContext;
using PizzaShop.Domain.ViewModels;
using PIzzaShop.Service;

namespace PizzaShop.Web.Controllers;

public class ResetPasswordController : Controller
{
    private readonly PizzaShopDbContext _context;

    private readonly IGenerateJwt _generateJwt;
    public ResetPasswordController( PizzaShopDbContext context,IGenerateJwt generateJwt)
    {
        _context=context;
        _generateJwt=generateJwt;
    }
  
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [Route("resetpassword")]
    public IActionResult ResetPassword(string token)
    {
        // extract the user id from the token
        var id = int.Parse(token.Split("?id=")[1]);
        token = token.Split("?id=")[0];
        var storedToken = HttpContext.Session.GetString("ResetToken");
        var storedExpiry = HttpContext.Session.GetString("ResetTokenExpiry");

        if (storedToken == token && DateTime.Parse(storedExpiry) > DateTime.UtcNow)
        {
            return View(new ResetPasswordViewModel { Token = token, UserId = id });
        }
        else
        {
            return View("TokenExpired");
        }
    }

    [HttpPost]
    [Route("/api/resetpassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
    {
        try
        {
            var userId = model.UserId;
           
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                var storedToken = HttpContext.Session.GetString("ResetToken");
                var storedExpiry = HttpContext.Session.GetString("ResetTokenExpiry");

                if (storedToken == model.Token && DateTime.Parse(storedExpiry) > DateTime.UtcNow)
                {

                    user.Password=BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
                    _context.Update(user);
                    _context.SaveChanges();

                    // Clear the session/cache
                    HttpContext.Session.Remove("ResetToken");
                    HttpContext.Session.Remove("ResetTokenExpiry");

                    return new JsonResult(new { success = true, message = "Password reset successfully" });
                }
                else
                {
                    return new JsonResult(new { success = false, message = "Invalid or expired token" });
                }
            }
            else
            {
                return new JsonResult(new { success = false, message = "User not found" });
            }
        }
        catch (Exception ex)
        {
            return new JsonResult(new { success = false, message = ex.Message });
        }
    }


    [HttpGet]
    public  async Task<IActionResult> ResetPasswordAtFirstLogin()
    {
        return View();
    }

     [HttpPost]
     [Route("/api/resetpasswordatFirstLogin")]
    public  async Task<IActionResult> ResetPasswordAtFirstLogin([FromBody] ResetPasswordAtFirstLogin model)
    {
       var userid= _generateJwt.GetUserIdFromJwtToken(Request.Cookies["token"]);
       var user=await _context.Users.FirstOrDefaultAsync(u=>u.Id==userid);
         if(user!=null)
         {
              user.Password=BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
              user.HasLoggedInBefore=true;
              _context.Update(user);
              _context.SaveChanges();
              return new JsonResult(new { success = true, message = "Password reset successfully" });
         }
         else
         {
              return new JsonResult(new { success = false, message = "User not found" });
         }
    }


}

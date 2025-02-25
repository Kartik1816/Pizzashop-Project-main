using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.ViewModels;
using PIzzaShop.Service.Interfaces;
using PIzzaShop.Service.Services;

namespace PizzaShop.Web.Controllers;


   public class ForgotPasswordController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IForgotPasswordService _forgotPasswordService;
        

        public ForgotPasswordController(EmailService emailService,IForgotPasswordService forgotPasswordService)
        {
            _emailService = emailService;
            _forgotPasswordService=forgotPasswordService;
        }

        public IActionResult Index()
        {
            return View();
        }

    [HttpPost]
    [Route("api/forgotpassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel forgotPasswordModel)
        {
            try
            {
                var email = forgotPasswordModel.Email;
                var user = await _forgotPasswordService.getUserFromEmail(email);
                if (user != null)
                {
                    // Generate a unique token
                    var token = Guid.NewGuid().ToString();
                    var expiryDate = DateTime.UtcNow.AddDays(1); // Token valid for 1 day
                    HttpContext.Session.SetString("ResetToken", token);
                    HttpContext.Session.SetString("ResetTokenExpiry", expiryDate.ToString());

                    var resetLink = $"http://localhost:5138/resetpassword?token={token}?id={user.Id}";
                    await _emailService.SendEmailAsync(user.Email, user, resetLink);

                    return new JsonResult(new { success = true, message = "Email Sent" });
                }
                else
                {
                    return new JsonResult(new { success = false, message = "Email not registered" });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }
}



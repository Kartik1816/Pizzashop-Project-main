using System.ComponentModel.DataAnnotations;
namespace PizzaShop.Domain.ViewModels;

public class UserViewModel
{
        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(128, ErrorMessage = "Email cannot exceed 50 characters.")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Please enter correct email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 6 characters.")]
        [MaxLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",ErrorMessage ="Please Enter correct Password")]
        public string? Password { get; set; }
}

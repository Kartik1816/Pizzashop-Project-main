using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Domain.ViewModels;

public class ChangePasswordViewModel
{
        [Required(ErrorMessage = "Old Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 6 characters.")]
        [MaxLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",ErrorMessage ="Please Enter correct Password")]
        [DataType(DataType.Password)]
        public string? OldPassword { get; set; }


        [Required(ErrorMessage = "New  Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 6 characters.")]
        [MaxLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",ErrorMessage ="Please Enter correct Password")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }


        [Required(ErrorMessage = "Confirm Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 6 characters.")]
        [MaxLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",ErrorMessage ="Please Enter correct Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Password does not match")]
        public string? ConfirmPassword { get; set; }

        public string? UserName { get; set; }

        public string? ProfileImageURL { get; set; }
}

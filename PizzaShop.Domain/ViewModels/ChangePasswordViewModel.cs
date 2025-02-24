using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Domain.ViewModels;

public class ChangePasswordViewModel
{
     [Required]
        [DataType(DataType.Password)]
        public string? OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Password does not match")]
        public string? ConfirmPassword { get; set; }
}

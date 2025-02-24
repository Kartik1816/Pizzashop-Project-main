using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Domain.ViewModels;

public class EditProfileViewModel
{
     [Required]
    public string FirstName { get; set; }
    public string? LastName { get; set; }

    [Required]
    public string UserName { get; set; }
    public string? Phone { get; set; }
    public int CountryId { get; set; }
    public int StateId { get; set; }
    public int CityId { get; set; }

    public string? Address { get; set; }

    [Required]
    public string ZipCode { get; set; }

    public string? Url { get; set; }

    public string? Role { get; set; }

    public string? Email { get; set; }
}

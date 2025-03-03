using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PizzaShop.Domain.ViewModels;

public class EditUserViewModel
{
        public int Id { get; set; }
        [Required(ErrorMessage = "FirstName is Required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(128, ErrorMessage = "Email cannot exceed 50 characters.")]
        [EmailAddress (ErrorMessage = "Please Enter correct Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phonenumber is Required")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "ZipCode is required.")]
        public string ZipCode { get; set; }
        public bool Status { get; set; }
        public int RoleIdRequestedUser { get; set; }
        public string? UserName { get; set; }
        public string? ProfileImageURL { get; set; }
        [Required(ErrorMessage = "Country is required.")]
        public int CountryId { get; set; }
        [Required(ErrorMessage = "State is required.")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "City is required.")]
        public int CityId { get; set; }
        [Required (ErrorMessage = "Username is required.")]
        public string UsernameRequestedUSer { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public int RoleId { get; set; }      
}


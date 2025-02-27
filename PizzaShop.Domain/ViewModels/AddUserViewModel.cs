using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace PizzaShop.Domain.ViewModels;

public class AddUserViewModel
{
        public int Id {get; set; }
        [Required(ErrorMessage ="FirstName is Required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage ="Email is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Phonenumber is Required")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Password { get; set; }
        public int RoleIdRequestedUser { get; set; }
        public string UserName { get; set; }
        public string ProfileImageURL { get; set; }
        
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string UsernameRequestedUser { get; set; }
        public IFormFile ProfileImage { get; set; }
        public int RoleId { get; set; }      
}

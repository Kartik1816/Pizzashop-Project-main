using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PizzaShop.Domain.ViewModels;

public class EditUserViewModel
{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public bool Status { get; set; }
        public int RoleIdRequestedUser { get; set; }
        public string UserName { get; set; }
        public string ProfileImageURL { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string UsernameRequestedUSer { get; set; }
        public IFormFile ProfileImage { get; set; }
        public int RoleId { get; set; }      
}


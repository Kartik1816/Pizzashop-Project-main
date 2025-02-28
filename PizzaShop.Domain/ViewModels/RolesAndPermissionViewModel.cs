using PizzaShop.Domain.Models;

namespace PizzaShop.Domain.ViewModels;

public class RolesAndPermissionViewModel
{
    public List<Role> Roles { get; set; }
    public string? UserName { get; set; }
    public string? ProfileImageURL { get; set; }
}

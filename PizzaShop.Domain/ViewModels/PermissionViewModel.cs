using PizzaShop.Domain.Models;

namespace PizzaShop.Domain.ViewModels;

public class PermissionViewModel
{
    public string UserName { get; set; }

    public string ProfileImageURL { get; set; }

    public Role role { get; set; }

    public List<Permission> Permissions { get; set; }

    public List<RolePermission> RolePermissions { get; set; }

}

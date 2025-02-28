using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.Models;
using PizzaShop.Domain.ViewModels;

namespace PizzaShop.Repository.Interfaces;

public interface IRolePermissionRepository
{
    public Task<List<Permission>> getPermissions();
    public Task<List<RolePermission>> getRolePermissions(int roleId);

    public Task<Role> getRole(int roleId);

    public Task<IActionResult> savePermissions(List<SavePermissionModel> savePermissionModels);
}

using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.Models;
using PizzaShop.Domain.ViewModels;

namespace PIzzaShop.Service.Interfaces;

public interface IRolePermissionService
{
    public Task<List<Permission>> getPermissions();
    public Task<List<RolePermission>> getRolePermissions(int roleId);

    public Task<Role> getRole(int roleId);

    public Task<IActionResult> savePermissions(List<SavePermissionModel> savePermissionModels);

    public Task<List<RolePermissionModel>>getPermissionOfRole(int roleId);
}

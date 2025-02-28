using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.Models;
using PizzaShop.Domain.ViewModels;
using PizzaShop.Repository.Interfaces;
using PIzzaShop.Service.Interfaces;

namespace PIzzaShop.Service.Services;

public class RolePermissionService : IRolePermissionService
{
    public IRolePermissionRepository _rolePermissionRepository;
    public RolePermissionService(IRolePermissionRepository rolePermissionRepository)
    {
        _rolePermissionRepository = rolePermissionRepository;
    }
    public async Task<List<Permission>> getPermissions()
    {
        return await _rolePermissionRepository.getPermissions();
    }
    public async Task<List<RolePermission>> getRolePermissions(int roleId)
    {
        return await _rolePermissionRepository.getRolePermissions(roleId);
    }
    public async Task<Role> getRole(int roleId)
    {
        return await _rolePermissionRepository.getRole(roleId);
    }

    public async Task<IActionResult> savePermissions(List<SavePermissionModel> savePermissionModels)
    {
        return await _rolePermissionRepository.savePermissions(savePermissionModels);
    }
}

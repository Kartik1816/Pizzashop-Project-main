using System.Runtime.ExceptionServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Domain.DBContext;
using PizzaShop.Domain.Models;
using PizzaShop.Domain.ViewModels;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Repository;

public class RolePermissionRepository :IRolePermissionRepository
{
    public PizzaShopDbContext _pizzaShopDbContext;
    public RolePermissionRepository(PizzaShopDbContext pizzaShopDbContext)
    {
        _pizzaShopDbContext = pizzaShopDbContext;
    }
    public async Task<List<Permission>> getPermissions()
    {
        return  _pizzaShopDbContext.Permissions.ToList();
    }

    public async Task<List<RolePermission>> getRolePermissions(int roleId)
    {
        return _pizzaShopDbContext.RolePermissions.Where(r => r.RoleId == roleId).ToList();
    }

    public async Task<Role> getRole(int roleId)
    {
        return _pizzaShopDbContext.Roles.FirstOrDefault(r => r.Id == roleId);
    }
    public async Task<IActionResult> savePermissions(List<SavePermissionModel> savePermissionModels)
    {
        var roleId = savePermissionModels[0].RoleId;

        foreach (var rolePermission in savePermissionModels)
        {
            RolePermission obj = await _pizzaShopDbContext.RolePermissions.FirstOrDefaultAsync(r => r.RoleId == rolePermission.RoleId && r.PermissionId == rolePermission.PermissionId);

            if (obj == null)
            {
                return new JsonResult(new { success = false, message = "RolePermission not found" });
            }

            if (rolePermission.PermissionType == "CanView")
            {
                obj.CanView = rolePermission.PermissionValue;
                _pizzaShopDbContext.Update(obj);
                await _pizzaShopDbContext.SaveChangesAsync();
            }
            else if (rolePermission.PermissionType == "CanAddEdit")
            {
                obj.CanAddEdit = rolePermission.PermissionValue;
                _pizzaShopDbContext.Update(obj);
                await _pizzaShopDbContext.SaveChangesAsync();
            }
            else if (rolePermission.PermissionType == "CanDelete")
            {
                obj.CanDelete = rolePermission.PermissionValue;
                _pizzaShopDbContext.Update(obj);
                await _pizzaShopDbContext.SaveChangesAsync();
            }
            else
            {
                return new JsonResult(new { success = false, message = "Error Occured while Proceeding your Req" });
            }
        }

        return new JsonResult(new { success = true, message = "Permissions saved successfully" });
    }
}

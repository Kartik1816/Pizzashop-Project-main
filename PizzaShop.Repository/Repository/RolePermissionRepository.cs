using System.Runtime.ExceptionServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Domain.DBContext;
using PizzaShop.Domain.Models;
using PizzaShop.Domain.ViewModels;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Repository;

public class RolePermissionRepository : IRolePermissionRepository
{
    public PizzaShopDbContext _pizzaShopDbContext;

    private readonly IHttpContextAccessor _httpContextAccessor;
    public RolePermissionRepository(PizzaShopDbContext pizzaShopDbContext, IHttpContextAccessor httpContextAccessor)
    {
        _pizzaShopDbContext = pizzaShopDbContext;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<List<Permission>> getPermissions()
    {
        return _pizzaShopDbContext.Permissions.ToList();
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

        var roleId2 = _httpContextAccessor.HttpContext.Session.GetInt32("RoleId");
        if (roleId2 == roleId)
        {
            var rolePermissions = _pizzaShopDbContext.RolePermissions.Where(r => r.RoleId == roleId).ToList();
            var rolePermissionList = new List<RolePermissionModel>();
            foreach (var rolePermission in rolePermissions)
            {
                var permission = _pizzaShopDbContext.Permissions.FirstOrDefault(p => p.Id == rolePermission.PermissionId);
                RolePermissionModel rolePermissionModel = new RolePermissionModel
                {
                    PermissionId = permission.Id,
                    PermissionName = permission.Name,
                    CanView = (bool)rolePermission.CanView,
                    CanAddEdit = (bool)rolePermission.CanAddEdit,
                    CanDelete = (bool)rolePermission.CanDelete
                };
                rolePermissionList.Add(rolePermissionModel);
            }

            // save in session
            var permissionsBytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(rolePermissionList);
            _httpContextAccessor.HttpContext.Session.Set("Permissions", permissionsBytes);
        }

        return new JsonResult(new { success = true, message = "Permissions saved successfully" });
    }

    public async Task<List<RolePermissionModel>> getPermissionOfRole(int roleId)
    {
        var rolePermissions = _pizzaShopDbContext.RolePermissions.Where(r => r.RoleId == roleId).OrderBy(r => r.PermissionId).ToList();
        List<RolePermissionModel> rolePermissionModels = new List<RolePermissionModel>();
        foreach (var rolePermission in rolePermissions)
        {
            var permission = _pizzaShopDbContext.Permissions.FirstOrDefault(p => p.Id == rolePermission.PermissionId);
            if (permission != null)
            {

                RolePermissionModel rolePermissionModel = new RolePermissionModel
                {
                    PermissionId = rolePermission.PermissionId,
                    PermissionName = permission.Name,
                    CanView = (bool)rolePermission.CanView,
                    CanAddEdit = (bool)rolePermission.CanAddEdit,
                    CanDelete = (bool)rolePermission.CanDelete

                };
                rolePermissionModels.Add(rolePermissionModel);
            }

        }

        return rolePermissionModels;
    }
}

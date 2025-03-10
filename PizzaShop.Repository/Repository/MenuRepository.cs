using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PizzaShop.Domain.DBContext;
using PizzaShop.Domain.Models;
using PizzaShop.Domain.ViewModels;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Repository;

public class MenuRepository : IMenuRepository
{
    private readonly PizzaShopDbContext _pizzaShopDbContext;

    public MenuRepository(PizzaShopDbContext pizzaShopDbContext)
    {
        _pizzaShopDbContext = pizzaShopDbContext;
    }
    public async Task<List<Category>> getCategories()
    {
        return _pizzaShopDbContext.Categories.ToList();
    }

    public async Task<List<MenuItem>> getItems(int categoryId)
    {
        return _pizzaShopDbContext.MenuItems.Where(m => m.CategoryId == categoryId).ToList();
    }
    public async Task<List<MenuItem>> getItemsBySearch(int categoryIdm, string searchTerm)
    {
        if (searchTerm != null)
        {
            return _pizzaShopDbContext.MenuItems.Where(m => m.CategoryId == categoryIdm && m.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
        }
        return _pizzaShopDbContext.MenuItems.Where(m => m.CategoryId == categoryIdm).ToList();
    }
    public async Task<JsonResult> addCategory(Category category)
    {
        if (category != null)
        {
            if (category.Name == null)
            {
                return new JsonResult(new { success = false, message = "Category name is required" });
            }
            string name = _pizzaShopDbContext.Categories.FirstOrDefault(c => c.Name == category.Name)?.Name;
            if (name != null)
            {
                return new JsonResult(new { success = false, message = "Category already exists" });
            }
            _pizzaShopDbContext.Categories.Add(category);
            _pizzaShopDbContext.SaveChanges();
            return new JsonResult(new { success = true, message = "Category added successfully" });
        }
        else
        {
            return new JsonResult(new { success = false, message = "Category is null" });
        }
    }

    public async Task<JsonResult> updateCategory(Category category, int userId)
    {
        if (category != null)
        {
            Category cat = _pizzaShopDbContext.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (category.Name == null)
            {
                return new JsonResult(new { success = false, message = "Category name is required" });
            }
            if (cat.Name != category.Name)
            {
                string name = _pizzaShopDbContext.Categories.FirstOrDefault(c => c.Name == category.Name)?.Name;
                if (name != null)
                {
                    return new JsonResult(new { success = false, message = "Category already exists" });
                }
            }
            if (cat != null)
            {
                cat.Name = category.Name;
                cat.Description = category.Description;
                cat.UpdatedBy = userId;
                cat.UpdatedAt = DateTime.Now;
                _pizzaShopDbContext.SaveChanges();
                return new JsonResult(new { success = true, message = "Category updated successfully" });
            }
            else
            {
                return new JsonResult(new { success = false, message = "Category not found" });
            }
        }
        else
        {
            return new JsonResult(new { success = false, message = "Category is null" });
        }
    }

    public async Task<Category> getCategoryById(int id)
    {
        return _pizzaShopDbContext.Categories.FirstOrDefault(c => c.Id == id);
    }

    public async Task<JsonResult> deleteCategory(int categoryId)
    {
        Category category = _pizzaShopDbContext.Categories.FirstOrDefault(c => c.Id == categoryId);
        if (category == null)
        {
            return new JsonResult(new { success = false, message = "Category not found" });
        }
        category.IsDeleted = true;
        _pizzaShopDbContext.SaveChanges();
        return new JsonResult(new { success = true, message = "Category deleted successfully" });
    }

    public async Task<List<ModifierGroup>> getModifierGroups()
    {
        return _pizzaShopDbContext.ModifierGroups.ToList();
    }

    public async Task<List<ModifierGroup>> getModifierGroups(List<int> modifierGroupIds)
    {
        return _pizzaShopDbContext.ModifierGroups.Where(m => modifierGroupIds.Contains(m.Id)).ToList();
    }
    public async Task<List<Modifier>> getModifiers(List<int> modifierGroupIds)
    {
        return _pizzaShopDbContext.ModifierMappings.Where(mp => modifierGroupIds.Contains(mp.ModifiergroupId)).Select(m => m.Modifier).ToList();
    }

    public async Task<List<ModifierMapping>> getModifierMappings(List<int> modifierGroupIds)
    {
        return _pizzaShopDbContext.ModifierMappings.Where(mp => modifierGroupIds.Contains(mp.ModifiergroupId)).ToList();
    }


    public async Task<string> addItem(AddMenuItemViewModel addMenuItemViewModel, int userId)
    {
        MenuItem item = new MenuItem
        {
            Name = addMenuItemViewModel.ItemName,
            CategoryId = addMenuItemViewModel.CategoryId,
            ItemType = addMenuItemViewModel.ItemType,
            Quantity = addMenuItemViewModel.Quantity,
            Rate = (int)addMenuItemViewModel.Rate,
            Unit = addMenuItemViewModel.Unit,
            Available = addMenuItemViewModel.Availabe,
            DefaultTax = addMenuItemViewModel.DefaultTax,
            TaxPercent = (int)addMenuItemViewModel.TaxPercentage,
            ShortCode = addMenuItemViewModel.ShortCode,
            Description = addMenuItemViewModel.Description,
            CreatedBy = userId,
            UpdatedBy = userId
        };

        if (addMenuItemViewModel.ItemImage != null)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(addMenuItemViewModel.ItemImage.Name);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/item-images", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                addMenuItemViewModel.ItemImage.CopyTo(fileStream);
            }
            item.ImageUrl = fileName;
        }

        _pizzaShopDbContext.Add(item);
        _pizzaShopDbContext.SaveChanges();
        return item.Name;
    }

    public async Task<IActionResult> UpdateItemModifierGroup(AddMenuItemViewModel addItemViewModel, string itemName, int userId, List<ModifierMinMaxModel> modifierMinMaxList)
    {
        if (userId == null)
        {
            return new JsonResult(new { success = false, message = "User not found" });
        }
        if (string.IsNullOrEmpty(addItemViewModel.selectedModifierGroups))
        {
            return new JsonResult(new { success = true, message = "Item added suceccefully" });
        }

        var item = _pizzaShopDbContext.MenuItems.FirstOrDefault(i => i.Name == itemName);
        if (item == null)
        {
            return new JsonResult(new { success = false, message = "Item not found" });
        }
        foreach (var modifierMinMax in modifierMinMaxList)
        {
            var itemModifierMapping = new ItemModifierGroup
            {
                Menuid = item.Id,
                ModifiergroupId = modifierMinMax.Id,
                MaxValue = (short)modifierMinMax.MaxValue,
                MinValue = (short)modifierMinMax.MinValue,
                CreatedBy = userId,
                UpdatedBy = userId
            };
            _pizzaShopDbContext.ItemModifierGroups.Add(itemModifierMapping);
            _pizzaShopDbContext.SaveChanges();
        }
        return new JsonResult(new { success = true, message = "Item added successfully" });
    }
    public async Task<IActionResult> changeAval(int id, int Availability)
    {
        MenuItem item = _pizzaShopDbContext.MenuItems.FirstOrDefault(i => i.Id == id);
        if (Availability == 1)
        {
            item.Available = true;

        }
        else
        {
            item.Available = false;
        }
        _pizzaShopDbContext.Update(item);
        _pizzaShopDbContext.SaveChanges();
        return new JsonResult(new { message = "Avalability change successfully" });

    }

    public async Task<MenuItem> getItemById(int id)
    {
        return _pizzaShopDbContext.MenuItems.FirstOrDefault(i => i.Id == id);
    }
    public async Task<List<ModifierGroup>> getSelectedModifierGroups(int id)
    {
        List<int> modifierGroupIds = _pizzaShopDbContext.ItemModifierGroups.Where(i => i.Menuid == id).Select(i => i.ModifiergroupId).ToList();
        return _pizzaShopDbContext.ModifierGroups
        .Where(mg => modifierGroupIds.Contains(mg.Id))
        .ToList();
    }
    public async Task<List<ModifierMapping>> getSelectedModifierMappings(int id)
    {
        List<int> modifierGroupIds = _pizzaShopDbContext.ItemModifierGroups.Where(i => i.Menuid == id).Select(i => i.ModifiergroupId).ToList();
        return _pizzaShopDbContext.ModifierMappings.Where(mp => modifierGroupIds.Contains(mp.ModifiergroupId)).ToList();
    }

    public async Task<List<Modifier>> getSelectedModifiers(int id)
    {
        List<int> modifierGroupIds = _pizzaShopDbContext.ItemModifierGroups.Where(i => i.Menuid == id).Select(i => i.ModifiergroupId).ToList();
        return _pizzaShopDbContext.ModifierMappings.Where(mp=>modifierGroupIds.Contains(mp.ModifiergroupId)).Select(m=>m.Modifier).ToList();
    }
    public async Task<List<ModifierMinMaxModel>> getMinMaxMod(int id)
    {
        List<int>getgrpIds=_pizzaShopDbContext.ItemModifierGroups.Where(i=>i.Menuid==id).Select(i=>i.ModifiergroupId).ToList();
        List<int> getMins = _pizzaShopDbContext.ItemModifierGroups.Where(i => i.Menuid == id).Select(i => (int)(i.MinValue ?? 0)).ToList();
        List<int> getMaxs = _pizzaShopDbContext.ItemModifierGroups.Where(i => i.Menuid == id).Select(i => (int)(i.MaxValue ?? 0)).ToList();
        List<ModifierMinMaxModel> modifierMinMaxModels = new List<ModifierMinMaxModel>();
        for(int i=0;i<getgrpIds.Count();i++)
        {
            modifierMinMaxModels.Add(new ModifierMinMaxModel { Id = getgrpIds[i], MinValue = getMins[i], MaxValue = getMaxs[i] });
        }
        return modifierMinMaxModels;
    }
}

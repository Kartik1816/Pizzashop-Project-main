using Microsoft.AspNetCore.Mvc;
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
    public async Task<List<MenuItem>> getItemsBySearch(int categoryIdm,string searchTerm)
    {
        if(searchTerm != null)
        {
            return _pizzaShopDbContext.MenuItems.Where(m => m.CategoryId == categoryIdm && m.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
        }
        return _pizzaShopDbContext.MenuItems.Where(m => m.CategoryId == categoryIdm).ToList();
    }
        public async Task<JsonResult> addCategory(Category category)
    {
        if(category != null)
        {
            if(category.Name == null)
            {
                return new JsonResult ( new {success=false,message="Category name is required"});
            }
            string name=_pizzaShopDbContext.Categories.FirstOrDefault(c=>c.Name==category.Name)?.Name;
            if(name != null)
            {
                return new JsonResult ( new {success=false,message="Category already exists"});
            }
            _pizzaShopDbContext.Categories.Add(category);
            _pizzaShopDbContext.SaveChanges();
            return new JsonResult ( new {success=true,message="Category added successfully"});
        }
        else
        {
            return new JsonResult ( new {success=false,message="Category is null"});
        }
    }

     public async Task<JsonResult> updateCategory(Category category,int userId)
     {
            if(category != null)
            {
                Category cat = _pizzaShopDbContext.Categories.FirstOrDefault(c => c.Id == category.Id);
                if(category.Name == null)
                {
                    return new JsonResult ( new {success=false,message="Category name is required"});
                }
                if(cat.Name != category.Name)
                {
                    string name=_pizzaShopDbContext.Categories.FirstOrDefault(c=>c.Name==category.Name)?.Name;
                    if(name != null)
                    {
                        return new JsonResult ( new {success=false,message="Category already exists"});
                    }
                }
                if(cat != null)
                {
                    cat.Name = category.Name;
                    cat.Description = category.Description;
                    cat.UpdatedBy = userId;
                    cat.UpdatedAt = DateTime.Now;
                    _pizzaShopDbContext.SaveChanges();
                    return new JsonResult ( new {success=true,message="Category updated successfully"});
                }
                else
                {
                    return new JsonResult ( new {success=false,message="Category not found"});
                }
            }
            else
            {
                return new JsonResult ( new {success=false,message="Category is null"});
            }
     }

     public async Task<Category> getCategoryById(int id)
     {
            return _pizzaShopDbContext.Categories.FirstOrDefault(c => c.Id == id);
     }

      public async Task<JsonResult> deleteCategory(int categoryId)
     {
        Category category =_pizzaShopDbContext.Categories.FirstOrDefault(c=>c.Id==categoryId);
            if (category == null)
            {
                return new JsonResult(new { success = false, message = "Category not found" });
            }
            category.IsDeleted= true;
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

    public async Task<MenuItem> getMenuItemByName(string name)
    {
        return _pizzaShopDbContext.MenuItems.FirstOrDefault(i=>i.Name == name);
    }

    public async Task<string> addItem(AddMenuItemViewModel addMenuItemViewModel,int userId)
    {
        MenuItem item= new MenuItem
        {
            Name=addMenuItemViewModel.ItemName,
            CategoryId=addMenuItemViewModel.CategoryId,
            ItemType=addMenuItemViewModel.ItemType,
            Quantity=addMenuItemViewModel.Quantity,
            Rate=(int)addMenuItemViewModel.Rate,
            Unit = addMenuItemViewModel.Unit,
            Available = addMenuItemViewModel.Availabe,
            DefaultTax = addMenuItemViewModel.DefaultTax,
            TaxPercent = (int)addMenuItemViewModel.TaxPercentage,
            
            Description = addMenuItemViewModel.Description,
            CreatedBy = userId,
            UpdatedBy = userId
        };

        // if (addMenuItemViewModel.ItemImage!= null)
        //     {
        //         var fileName = Guid.NewGuid().ToString() + Path.GetExtension(addMenuItemViewModel.ItemImage);
        //         var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/item-images", fileName);
        //         using (var fileStream = new FileStream(path, FileMode.Create))
        //         {
        //             addMenuItemViewModel.Image.CopyTo(fileStream);
        //         }
        //         item.ImageUrl = fileName;
        //     }

        _pizzaShopDbContext.Add(item);
        _pizzaShopDbContext.SaveChanges();
        return item.Name;
    }

    //  public IActionResult UpdateItemModifierGroup(AddMenuItemViewModel addItemViewModel, string itemName, int userId)
    //     {
    //         if (userId == null)
    //         {
    //             return new JsonResult(new { success = false, message = "User not found" });
    //         }
    //         if (string.IsNullOrEmpty(addItemViewModel.selectedModifierGroups))
    //         {
    //             return new JsonResult(new { success = true, message = "Item added suceccefully" });
    //         }
    //         List<QuantityObject> maximumQuantities = JsonConvert.DeserializeObject<List<QuantityObject>>(addItemViewModel.MaximumQuantity);
    //         List<QuantityObject> minimumQuantities = JsonConvert.DeserializeObject<List<QuantityObject>>(addItemViewModel.MinimumQuantity);
    //         List<int> modifierGroupIds = JsonConvert.DeserializeObject<List<int>>(addItemViewModel.ModifierGroupIds);
    //         var item = _context.Items.FirstOrDefault(i => i.Name == itemName);
    //         if (item == null)
    //         {
    //             return new JsonResult(new { success = false, message = "Item not found" });
    //         }
    //         foreach (var modifierGroupId in modifierGroupIds)
    //         {
    //             var itemModifierMapping = new ItemModifiergroup
    //             {
    //                 ItemId = item.Id,
    //                 ModifiergroupId = modifierGroupId,
    //                 MaxValue = (short?)int.Parse(maximumQuantities.FirstOrDefault(m => int.Parse(m.Id) == modifierGroupId).Value ?? "0"),
    //                 MinValue = (short?)int.Parse(minimumQuantities.FirstOrDefault(m => int.Parse(m.Id) == modifierGroupId).Value ?? "0"),
    //                 CreatedBy = userId,
    //                 UpdatedBy = userId
    //             };
    //             _context.ItemModifiergroups.Add(itemModifierMapping);
    //             _context.SaveChanges();
    //         }
    //         return new JsonResult(new { success = true, message = "Item added successfully" });
    //     }
}

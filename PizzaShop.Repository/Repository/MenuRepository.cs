using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.DBContext;
using PizzaShop.Domain.Models;
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
}

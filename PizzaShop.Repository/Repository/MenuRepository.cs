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
}

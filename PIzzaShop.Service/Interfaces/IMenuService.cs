using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.Models;

namespace PIzzaShop.Service.Interfaces;

public interface IMenuService
{
    public Task<List<Category>> getCategories();

    public Task<List<MenuItem>> getItems(int categoryId);

    public Task<List<MenuItem>> getItemsBySearch(int categoryId,string searchTerm);

    public Task<IActionResult> addCategory(Category category);
     public  Task<IActionResult> updateCategory(Category category,int userId);
    public  Task<Category> getCategoryById(int id);

    public Task<JsonResult> deleteCategory(int categoryId);
}

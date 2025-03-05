using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.Models;

namespace PizzaShop.Repository.Interfaces;

public interface IMenuRepository
{
    public Task<List<Category>> getCategories();

    public Task<List<MenuItem>> getItems(int categoryId);
    public Task<List<MenuItem>> getItemsBySearch(int categoryId,string searchTerm);
    public Task<JsonResult> addCategory(Category category);

    public  Task<JsonResult> updateCategory(Category category,int userId);
    public  Task<Category> getCategoryById(int id);

    public  Task<JsonResult> deleteCategory(int categoryId);

    public Task<List<ModifierGroup>> getModifierGroups();
}

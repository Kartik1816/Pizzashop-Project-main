using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.Models;
using PizzaShop.Domain.ViewModels;

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

     public Task<List<ModifierGroup>> getModifierGroups();
    public Task<List<ModifierGroup>> getModifierGroups(List<int> modifierGroupIds);
    public Task<List<Modifier>> getModifiers(List<int> modifierGroupIds);

    public Task<List<ModifierMapping>> getModifierMappings(List<int> modifierGroupIds);


    public  Task<string> addItem(AddMenuItemViewModel addMenuItemViewModel,int userId);
    public  Task<IActionResult> UpdateItemModifierGroup(AddMenuItemViewModel addItemViewModel, string itemName, int userId);
    public Task<IActionResult> changeAval(int id,int Availability);
}

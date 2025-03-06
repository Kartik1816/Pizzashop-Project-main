using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.Models;
using PizzaShop.Domain.ViewModels;
using PizzaShop.Repository.Interfaces;
using PIzzaShop.Service.Interfaces;

namespace PIzzaShop.Service.Services;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;

    public MenuService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }
    public async Task<List<Category>> getCategories()
    {
        return await _menuRepository.getCategories();
    }
    public Task<List<MenuItem>> getItems(int categoryId)
    {
        return _menuRepository.getItems(categoryId);
    }
     public Task<List<MenuItem>> getItemsBySearch(int categoryId,string searchTerm)
     {
         return _menuRepository.getItemsBySearch(categoryId,searchTerm);
     }

    public async Task<IActionResult> addCategory(Category category)
    {
        return await _menuRepository.addCategory(category);
    }

    public async Task<IActionResult> updateCategory(Category category,int userId)
    {
        return await _menuRepository.updateCategory(category,userId);
    }

    public async Task<Category> getCategoryById(int id)
    {
        return await _menuRepository.getCategoryById(id);
    }

    public Task<JsonResult> deleteCategory(int categoryId)
    {
        return _menuRepository.deleteCategory(categoryId);
    }

    public async Task<List<ModifierGroup>> getModifierGroups()
    {
        return await _menuRepository.getModifierGroups();
    }

    public Task<List<ModifierGroup>> getModifierGroups(List<int> modifierGroupIds)
    {
        return _menuRepository.getModifierGroups(modifierGroupIds);
    }
    public Task<List<Modifier>> getModifiers(List<int> modifierGroupIds)
    {
        return _menuRepository.getModifiers(modifierGroupIds);
    }

    public  async Task<List<ModifierMapping>> getModifierMappings(List<int> modifierGroupIds)
    {
        return await _menuRepository.getModifierMappings(modifierGroupIds);
    }
     public async Task<MenuItem> getMenuItemByName(string name)
    {
        return await _menuRepository.getMenuItemByName(name);
    }

    public  async Task<string> addItem(AddMenuItemViewModel addMenuItemViewModel,int userId)
    {
        return await _menuRepository.addItem(addMenuItemViewModel, userId);
    }
}

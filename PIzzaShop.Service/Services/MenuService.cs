using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.Models;
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

    public async Task<IActionResult> addCategory(Category category)
    {
        return await _menuRepository.addCategory(category);
    }
}

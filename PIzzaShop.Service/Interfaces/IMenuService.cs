using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.Models;

namespace PIzzaShop.Service.Interfaces;

public interface IMenuService
{
    public Task<List<Category>> getCategories();

    public Task<IActionResult> addCategory(Category category);
}

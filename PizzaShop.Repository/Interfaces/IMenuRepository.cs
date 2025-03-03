using Microsoft.AspNetCore.Mvc;
using PizzaShop.Domain.Models;

namespace PizzaShop.Repository.Interfaces;

public interface IMenuRepository
{
    public Task<List<Category>> getCategories();
   public Task<JsonResult> addCategory(Category category);
}

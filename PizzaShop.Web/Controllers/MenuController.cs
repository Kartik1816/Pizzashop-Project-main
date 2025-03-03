using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;
using PizzaShop.Domain.Models;
using PizzaShop.Domain.ViewModels;
using PIzzaShop.Service;
using PIzzaShop.Service.Interfaces;

namespace PizzaShop.Web.Controllers;

public class MenuController : Controller
{
    private readonly IMenuService _menuService;

    private readonly IGenerateJwt _generateJwt;
 
    public MenuController(IMenuService menuService, IGenerateJwt generateJwt)
    {
        _menuService = menuService;
        _generateJwt = generateJwt;
    }
  
    public IActionResult Index()
    {
        CategoryViewModel categoryViewModel = new CategoryViewModel();
        categoryViewModel.Categories = _menuService.getCategories().Result;
        return View(categoryViewModel);
    }

    [HttpPost]
    [Route("/Menu/AddCategory")]
    public async Task<IActionResult> AddCategory([FromBody] CategoryViewModel categoryViewModel)
    {
        int userId=_generateJwt.GetUserIdFromJwtToken(Request.Cookies["token"]);
        Category category= new Category{
            Name = categoryViewModel.CategoryName,
            Description = categoryViewModel.Description,
            CreatedAt=DateTime.Now,
            UpdatedAt=DateTime.Now,
            CreatedBy=userId,
            UpdatedBy=userId
        };
       return await _menuService.addCategory(category);
    }
}

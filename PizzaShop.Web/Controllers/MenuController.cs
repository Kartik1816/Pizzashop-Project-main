using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
  
    public async Task<IActionResult> Index()
    {
        CategoryViewModel categoryViewModel = new CategoryViewModel{};
        categoryViewModel.Categories = await _menuService.getCategories();
        categoryViewModel.MenuItems=await _menuService.getItems(categoryViewModel.Categories[0].Id);

        categoryViewModel.TotalItems=categoryViewModel.MenuItems.Count;
        
        int allItemCount=categoryViewModel.MenuItems.Count;
        
        categoryViewModel.MenuItems=categoryViewModel.MenuItems.Take(5).ToList();
        
        categoryViewModel.PageIndex=1;
        categoryViewModel.PageSize=5;
        
        categoryViewModel.TotalPages=(int)Math.Ceiling((double)allItemCount/categoryViewModel.PageSize);

        categoryViewModel.ModifierGroups= await _menuService.getModifierGroups();
        return View(categoryViewModel);
    }


[HttpGet]

 public async Task<IActionResult> getMenuItems(int PageIndex,int PageSize,string searchTerm=null)
    {
        CategoryViewModel categoryViewModel = new CategoryViewModel();
        categoryViewModel.Categories = await _menuService.getCategories();
        
        categoryViewModel.MenuItems=await _menuService.getItemsBySearch(categoryViewModel.Categories[0].Id,searchTerm);
        categoryViewModel.TotalItems=categoryViewModel.MenuItems.Count;
        int allItemCount=categoryViewModel.MenuItems.Count;
        categoryViewModel.MenuItems=categoryViewModel.MenuItems.Skip((PageIndex-1)*PageSize).Take(PageSize).ToList();
        categoryViewModel.PageIndex=PageIndex;
        categoryViewModel.PageSize=PageSize;
        
        categoryViewModel.TotalPages=(int)Math.Ceiling((double)allItemCount/categoryViewModel.PageSize);
        return PartialView("_Items",categoryViewModel);
    }

[HttpGet]

 public async Task<IActionResult> getMenuItemsBySearch(int PageIndex,int PageSize,int categoryId,string searchTerm=null)
    {
        CategoryViewModel categoryViewModel = new CategoryViewModel();
        categoryViewModel.Categories = await _menuService.getCategories();
        
        categoryViewModel.MenuItems=await _menuService.getItemsBySearch(categoryId,searchTerm);
        categoryViewModel.TotalItems=categoryViewModel.MenuItems.Count;
        int allItemCount=categoryViewModel.MenuItems.Count;
        categoryViewModel.MenuItems=categoryViewModel.MenuItems.Skip((PageIndex-1)*PageSize).Take(PageSize).ToList();
        categoryViewModel.PageIndex=PageIndex;
        categoryViewModel.PageSize=PageSize;
        
        categoryViewModel.TotalPages=(int)Math.Ceiling((double)allItemCount/categoryViewModel.PageSize);
        return PartialView("_Items",categoryViewModel);
    }

    [HttpGet]

 public async Task<IActionResult> getMenuItemsFilterByCategory(int PageIndex,int PageSize,int categoryId,string searchTerm=null)
    {
        CategoryViewModel categoryViewModel = new CategoryViewModel();
        categoryViewModel.Categories = await _menuService.getCategories();
        
        categoryViewModel.MenuItems=await _menuService.getItemsBySearch(categoryId,searchTerm);
        categoryViewModel.TotalItems=categoryViewModel.MenuItems.Count;
        int allItemCount=categoryViewModel.MenuItems.Count;
        categoryViewModel.MenuItems=categoryViewModel.MenuItems.Skip((PageIndex-1)*PageSize).Take(PageSize).ToList();
        categoryViewModel.PageIndex=PageIndex;
        categoryViewModel.PageSize=PageSize;
        
        categoryViewModel.TotalPages=(int)Math.Ceiling((double)allItemCount/categoryViewModel.PageSize);
        return PartialView("_Items",categoryViewModel);
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

    
    [HttpPost]
    [Route("/Menu/EditCategory/{id}")]
    public async Task<IActionResult> EditCategory([FromBody] CategoryViewModel categoryViewModel,int id)
    {
        int userId=_generateJwt.GetUserIdFromJwtToken(Request.Cookies["token"]);
        Category category=await _menuService.getCategoryById(id);
        category.Name = categoryViewModel.CategoryName;
        category.Description = categoryViewModel.Description;

        return await _menuService.updateCategory(category,userId);
    }

        [HttpDelete]
        [Route("Menu/DeleteCategory/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
             return await _menuService.deleteCategory(categoryId);

        }

    [HttpGet]
    public async Task<IActionResult> selectModifier(string selectedItems)
    {
        if (selectedItems == "[]")
        {
            return PartialView("_Modifier", new CategoryViewModel());
        }
        
        List<int> selectedItemsList = selectedItems.Trim('[', ']').Split(',').Select(int.Parse).ToList();
        List<ModifierGroup> selectedModifierGroups = await _menuService.getModifierGroups(selectedItemsList);
        List<Modifier> selectedModifiers = await _menuService.getModifiers(selectedItemsList);
        List<ModifierMapping> selectedModifierMappings = await _menuService.getModifierMappings(selectedItemsList);

        CategoryViewModel categoryViewModel = new CategoryViewModel
        {
            SelectedModifierGroups = selectedModifierGroups,
            SelectedModifiers = selectedModifiers,
            SelectedModifierMappings = selectedModifierMappings
        };

        return PartialView("_Modifier", categoryViewModel);

    }
    
    [HttpPost]
    public  async Task<IActionResult> AddItem([FromForm] AddMenuItemViewModel addMenuItemViewModel)
    {
        int userId=_generateJwt.GetUserIdFromJwtToken(Request.Cookies["token"]);
        List<ModifierMinMaxModel> modifierMinMaxList = JsonConvert.DeserializeObject<List<ModifierMinMaxModel>>(addMenuItemViewModel.selectedModifierGroups);
        string name=await _menuService.addItem(addMenuItemViewModel,userId);
        MenuItem menuItem= await _menuService.getMenuItemByName(name);
        return Json("asdfasd");
    }

}

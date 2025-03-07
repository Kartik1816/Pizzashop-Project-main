using System.ComponentModel.DataAnnotations;
using PizzaShop.Domain.Models;

namespace PizzaShop.Domain.ViewModels;

public class CategoryViewModel
{
    public List<Category> Categories { get; set; }

    [Required (ErrorMessage = "Category Name is required")]
    public string CategoryName { get; set; }
    public string? Description { get; set; }

    public  List<MenuItem> MenuItems { get; set; }

    public int PageIndex { get; set; }

    public int TotalPages { get; set; }

    public int TotalItems { get; set; }

    public int PageSize { get; set; }

    public List<Modifier> SelectedModifiers { get; set; }

    public List<ModifierGroup> ModifierGroups { get; set; }
    public List<ModifierGroup> SelectedModifierGroups { get; set; }

    public List<ModifierMapping> SelectedModifierMappings { get; set; }

    public AddMenuItemViewModel addMenuItemViewModel { get; set; }

    public List<ModifierMinMaxModel> modifierMinMaxModels{ get; set; }
}

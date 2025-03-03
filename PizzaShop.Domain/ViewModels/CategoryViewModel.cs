using System.ComponentModel.DataAnnotations;
using PizzaShop.Domain.Models;

namespace PizzaShop.Domain.ViewModels;

public class CategoryViewModel
{
    public List<Category> Categories { get; set; }
        [Required (ErrorMessage = "Category Name is required")]
    public string CategoryName { get; set; }
    public string? Description { get; set; }

}

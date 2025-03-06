using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PizzaShop.Domain.ViewModels;

public class AddMenuItemViewModel
{
    [Required(ErrorMessage ="CategoryId is required")]
    public int CategoryId { get; set; }


    [Required (ErrorMessage ="Item Name is required")]
    public string ItemName { get; set; }

    [Required(ErrorMessage = "ItemType is required")]
    public string ItemType { get; set; }

    [Required(ErrorMessage ="Rate is required")]
    public double Rate { get; set; }

    [Required(ErrorMessage ="Quantity is required")]
    public int Quantity { get; set; }

    [Required(ErrorMessage ="Unit is Required")]
    public string Unit { get; set; }

    public bool Availabe { get; set; }

    public bool DefaultTax { get; set; }

    public double TaxPercentage { get; set; }

    public string? ShortCode { get; set; }

    public string? Description { get; set; }

    public IFormFile? ItemImage { get; set; }

    public string selectedModifierGroups { get; set; }

    public string maxValue { get; set; }
    public string minValue { get; set; }
    

}

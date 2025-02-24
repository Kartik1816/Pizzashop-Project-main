using System;
using System.Collections.Generic;

namespace PizzaShop.Domain.Models;

public partial class MenuItem
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    public decimal Rate { get; set; }

    public int? Quantity { get; set; }

    public string? Unit { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public bool? DefaultTax { get; set; }

    public bool? Available { get; set; }

    public decimal? TaxPercent { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User UpdatedByNavigation { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<ModifierGroup> Modifiergroups { get; set; } = new List<ModifierGroup>();

    public virtual ICollection<Modifier> Modifiers { get; set; } = new List<Modifier>();
}

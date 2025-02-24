using System;
using System.Collections.Generic;

namespace PizzaShop.Domain.Models;

public partial class OrderModifier
{
    public int Id { get; set; }

    public int OrderItemId { get; set; }

    public int ModifierId { get; set; }

    public decimal Price { get; set; }

    public virtual Modifier Modifier { get; set; } = null!;

    public virtual OrderItem OrderItem { get; set; } = null!;
}

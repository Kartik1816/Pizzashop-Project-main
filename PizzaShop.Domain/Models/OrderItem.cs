﻿using System;
using System.Collections.Generic;

namespace PizzaShop.Domain.Models;

public partial class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ItemId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual MenuItem Item { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<OrderModifier> OrderModifiers { get; set; } = new List<OrderModifier>();
}

﻿using System;
using System.Collections.Generic;

namespace PizzaShop.Domain.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

    public virtual User UpdatedByNavigation { get; set; } = null!;
}

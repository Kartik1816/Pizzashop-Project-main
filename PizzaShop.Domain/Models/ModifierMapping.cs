using System;
using System.Collections.Generic;

namespace PizzaShop.Domain.Models;

public partial class ModifierMapping
{
    public int ModifierId { get; set; }

    public int ModifiergroupId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Modifier Modifier { get; set; } = null!;

    public virtual ModifierGroup Modifiergroup { get; set; } = null!;
}

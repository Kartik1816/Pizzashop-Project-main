using System;
using System.Collections.Generic;

namespace PizzaShop.Domain.Models;

public partial class ItemModifierGroup
{
    public int ModifiergroupId { get; set; }

    public int Menuid { get; set; }

    public short? MinValue { get; set; }

    public short? MaxValue { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual MenuItem Menu { get; set; } = null!;

    public virtual ModifierGroup Modifiergroup { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }
}

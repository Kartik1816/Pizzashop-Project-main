using System;
using System.Collections.Generic;

namespace PizzaShop.Domain.Models;

public partial class ModifierGroup
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<ItemModifierGroup> ItemModifierGroups { get; set; } = new List<ItemModifierGroup>();

    public virtual ICollection<ModifierMapping> ModifierMappings { get; set; } = new List<ModifierMapping>();

    public virtual User UpdatedByNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace PizzaShop.Domain.Models;

public partial class Review
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int OrderId { get; set; }

    public short Food { get; set; }

    public short Services { get; set; }

    public short Ambience { get; set; }

    public string? Comments { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}

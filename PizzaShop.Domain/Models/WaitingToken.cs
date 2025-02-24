using System;
using System.Collections.Generic;

namespace PizzaShop.Domain.Models;

public partial class WaitingToken
{
    public int Id { get; set; }

    public int SectionId { get; set; }

    public int CustomerId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual Section Section { get; set; } = null!;

    public virtual User UpdatedByNavigation { get; set; } = null!;
}

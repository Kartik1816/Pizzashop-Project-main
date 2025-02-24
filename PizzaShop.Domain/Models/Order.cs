using System;
using System.Collections.Generic;

namespace PizzaShop.Domain.Models;

public partial class Order
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int? TableId { get; set; }

    public decimal TotalAmount { get; set; }

    public short? Rating { get; set; }

    public TimeOnly KotTime { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<OrderTaxis> OrderTaxes { get; set; } = new List<OrderTaxis>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Table? Table { get; set; }

    public virtual User UpdatedByNavigation { get; set; } = null!;
}

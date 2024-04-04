using System;
using System.Collections.Generic;

namespace SmartOffice.Entities;

public partial class Soorderdetailstab
{
    public string OrderDetailsId { get; set; } = null!;

    public string OrderId { get; set; } = null!;

    public int UserId { get; set; }

    public string DishId { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;

    public virtual Sodishtab Dish { get; set; } = null!;

    public virtual Soordertab Order { get; set; } = null!;

    public virtual Sousertab User { get; set; } = null!;
}

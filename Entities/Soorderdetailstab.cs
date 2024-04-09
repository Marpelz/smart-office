using System;
using System.Collections.Generic;

namespace SmartOffice.Entities;

public partial class SoOrderDetailsTab
{
    public string OrderDetailsId { get; set; } = null!;

    public string OrderId { get; set; } = null!;

    public int UserId { get; set; }

    public string DishId { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;

    public virtual SoDishTab Dish { get; set; } = null!;

    public virtual SoOrderTab Order { get; set; } = null!;

    public virtual SoUserTab User { get; set; } = null!;
}

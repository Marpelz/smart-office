using System;
using System.Collections.Generic;

namespace SmartOffice.Entities;

public partial class SoOrderTab
{
    public string OrderId { get; set; } = null!;

    public string RestaurantId { get; set; } = null!;

    public int UserId { get; set; }

    public string OrderDate { get; set; } = null!;

    public virtual SoRestTab Restaurant { get; set; } = null!;

    public virtual ICollection<SoOrderDetailsTab> SoOrderDetailsTabs { get; set; } = new List<SoOrderDetailsTab>();

    public virtual SoUserTab User { get; set; } = null!;
}

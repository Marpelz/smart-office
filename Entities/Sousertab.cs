using System;
using System.Collections.Generic;

namespace SmartOffice.Entities;

public partial class SoUserTab
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserPassword { get; set; }

    public bool ActivePaypal { get; set; }

    public string? PaypalEmail { get; set; }

    public virtual ICollection<SoOrderDetailsTab> SoOrderDetailsTabs { get; set; } = new List<SoOrderDetailsTab>();

    public virtual ICollection<SoOrderTab> SoOrderTabs { get; set; } = new List<SoOrderTab>();
}

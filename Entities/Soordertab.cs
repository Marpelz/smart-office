using System;
using System.Collections.Generic;

namespace SmartOffice.Entities;

public partial class Soordertab
{
    public string OrderId { get; set; } = null!;

    public string RestaurantId { get; set; } = null!;

    public int UserId { get; set; }

    public string OrderDate { get; set; } = null!;

    public virtual Soresttab Restaurant { get; set; } = null!;

    public virtual ICollection<Soorderdetailstab> Soorderdetailstabs { get; set; } = new List<Soorderdetailstab>();

    public virtual Sousertab User { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace SmartOffice.Entities;

public partial class Soresttab
{
    public string RestId { get; set; } = null!;

    public string RestName { get; set; } = null!;

    public string RestStreet { get; set; } = null!;

    public string RestZipcode { get; set; } = null!;

    public string RestCity { get; set; } = null!;

    public string RestType { get; set; } = null!;

    public string RestPhonenumber { get; set; } = null!;

    public string RestDelivery { get; set; } = null!;

    public string RestDeliveryTime { get; set; } = null!;

    public string RestAppTelephone { get; set; } = null!;

    public string RestMinOrderValue { get; set; } = null!;

    public string RestDeliveryCost { get; set; } = null!;

    public string RestLieferandoLink { get; set; } = null!;

    public virtual ICollection<Sodishtab> Sodishtabs { get; set; } = new List<Sodishtab>();
}

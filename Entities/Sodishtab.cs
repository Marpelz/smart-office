using System;
using System.Collections.Generic;

namespace SmartOffice.Entities;

public partial class SoDishTab
{
    public string DishId { get; set; } = null!;

    public bool IsSelected { get; set; }

    public string DishRestId { get; set; } = null!;

    public string DishNumber { get; set; } = null!;

    public string DishCategory { get; set; } = null!;

    public string DishDesignation { get; set; } = null!;

    public string DishContents { get; set; } = null!;

    public string DishAdditionalSelection { get; set; } = null!;

    public string DishPrice { get; set; } = null!;

    public virtual SoRestTab DishRest { get; set; } = null!;

    public virtual ICollection<SoOrderDetailsTab> SoOrderDetailsTabs { get; set; } = new List<SoOrderDetailsTab>();
}

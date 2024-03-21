using System;
using System.Collections.Generic;

namespace SmartOffice.Entities;

public partial class Somenutab
{
    public string MenuId { get; set; } = null!;

    public string MenuRestId { get; set; } = null!;

    public string MenuFoodNumber { get; set; } = null!;

    public string MenuFoodCategory { get; set; } = null!;

    public string MenuFoodDesignation { get; set; } = null!;

    public string MenuFoodContents { get; set; } = null!;

    public string MenuFoodAdditionalSelection { get; set; } = null!;

    public string MenuFoodPrice { get; set; } = null!;
}

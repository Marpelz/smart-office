using System;
using System.Collections.Generic;

namespace SmartOffice.Entities;

public partial class Sousertab
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserPassword { get; set; }

    public bool ActivePaypal { get; set; }

    public string? PaypalEmail { get; set; }
}

using System;
using System.Collections.Generic;

namespace PRESCRIPTIONS_RX.Models;

public partial class PharmaciesAddress
{
    public int SN { get; set; }

    public string? PharmacyName { get; set; }

    public int PlotNumber { get; set; }

    public string? Street { get; set; }

    public string City { get; set; } = null!;

    public virtual Pharmacy? PharmacyNameNavigation { get; set; }
}

using System;
using System.Collections.Generic;

namespace PRESCRIPTIONS_RX.Models;

public partial class PatientAddress
{
    public int SN { get; set; }

    public string? PatientSsn { get; set; }

    public int PlotNumber { get; set; }

    public string? Street { get; set; }

    public string City { get; set; } = null!;

    public virtual Patient? PatientSsnNavigation { get; set; }
}

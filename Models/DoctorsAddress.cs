using System;
using System.Collections.Generic;

namespace PRESCRIPTIONS_RX.Models;

public partial class DoctorsAddress
{
    public int SN { get; set; }

    public string? DoctorSsn { get; set; }

    public int PlotNumber { get; set; }

    public string? Street { get; set; }

    public string City { get; set; } = null!;

    public virtual Doctor? DoctorSsnNavigation { get; set; }
}

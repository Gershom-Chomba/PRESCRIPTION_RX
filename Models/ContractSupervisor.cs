using System;
using System.Collections.Generic;

namespace PRESCRIPTIONS_RX.Models;

public partial class ContractSupervisor
{
    public int SN { get; set; }

    public string SupervisorSsn { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public int PlotNumber { get; set; }

    public string? Street { get; set; }

    public string City { get; set; } = null!;

    public string? Gender { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public int? ContractId { get; set; }

    public string? PharmacyName { get; set; }

    public virtual Contract? Contract { get; set; }

    public virtual Pharmacy? PharmacyNameNavigation { get; set; }
}

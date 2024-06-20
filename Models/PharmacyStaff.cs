using System;
using System.Collections.Generic;

namespace PRESCRIPTIONS_RX.Models;

public partial class PharmacyStaff
{
    public int StaffId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public int PlotNumber { get; set; }

    public string? Street { get; set; }

    public string City { get; set; } = null!;

    public string? Gender { get; set; }

    public string? StaffPassword { get; set; }

    public string EmailAddress { get; set; } = null!;
}

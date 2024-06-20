using System;
using System.Collections.Generic;

namespace PRESCRIPTIONS_RX.Models;

public partial class Patient
{
    public int SN { get; set; }

    public string PatientSsn { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string? Gender { get; set; }

    public string? PrimaryPhysician { get; set; }

    public virtual ICollection<PatientAddress> PatientAddresses { get; set; } = new List<PatientAddress>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}

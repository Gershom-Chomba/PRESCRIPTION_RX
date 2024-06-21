using System;
using System.Collections.Generic;

namespace PRESCRIPTIONS_RX.Models;

public partial class Doctor
{
    public int SN { get; set; }

    public string DoctorsSsn { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string Specialty { get; set; } = null!;

    public int YearsofExperience { get; set; }

    public virtual ICollection<DoctorsAddress> DoctorsAddresses { get; set; } = new List<DoctorsAddress>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}

using System;
using System.Collections.Generic;

namespace PRESCRIPTIONS_RX.Models;

public partial class PharmaceuticalCompany
{
    public int SN { get; set; }

    public string CompanyName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? DrugTradeName { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}

using System;
using System.Collections.Generic;

namespace PRESCRIPTIONS_RX.Models;

public partial class Contract
{
    public int ContractId { get; set; }

    public DateOnly Startdate { get; set; }

    public DateOnly Enddate { get; set; }

    public string? ContractText { get; set; }

    public string? PharmacyName { get; set; }

    public string? PharmaceuticalName { get; set; }

    public virtual ICollection<ContractSupervisor> ContractSupervisors { get; set; } = new List<ContractSupervisor>();

    public virtual PharmaceuticalCompany? PharmaceuticalNameNavigation { get; set; }

    public virtual Pharmacy? PharmacyNameNavigation { get; set; }
}

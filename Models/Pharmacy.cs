using System;
using System.Collections.Generic;

namespace PRESCRIPTIONS_RX.Models;

public partial class Pharmacy
{
    public int SN { get; set; }

    public string PharmacyName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? PharmaceuticalName { get; set; }

    public virtual ICollection<ContractSupervisor> ContractSupervisors { get; set; } = new List<ContractSupervisor>();

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual ICollection<PharmaciesAddress> PharmaciesAddresses { get; set; } = new List<PharmaciesAddress>();

    public virtual ICollection<SoldDrug> SoldDrugs { get; set; } = new List<SoldDrug>();
}

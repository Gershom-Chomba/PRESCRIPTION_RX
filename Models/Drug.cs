using System;
using System.Collections.Generic;

namespace PRESCRIPTIONS_RX.Models;

public partial class Drug
{
    public int SN { get; set; }

    public string TradeName { get; set; } = null!;

    public string Formula { get; set; } = null!;

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual ICollection<SoldDrug> SoldDrugs { get; set; } = new List<SoldDrug>();
}

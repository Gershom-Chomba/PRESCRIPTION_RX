using System;
using System.Collections.Generic;

namespace PRESCRIPTIONS_RX.Models;

public partial class Prescription
{
    public int PresciptionId { get; set; }

    public string? PatientSsn { get; set; }

    public string? PrescribingPhysician { get; set; }

    public DateOnly PrescriptionDate { get; set; }

    public int Quantity { get; set; }

    public string DrugTradeName { get; set; } = null!;

    public virtual Drug DrugTradeNameNavigation { get; set; } = null!;

    public virtual Patient? PatientSsnNavigation { get; set; }

    public virtual Doctor? PrescribingPhysicianNavigation { get; set; }
}

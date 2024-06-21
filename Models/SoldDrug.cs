using System;
using System.Collections.Generic;

namespace PRESCRIPTIONS_RX.Models;

public partial class SoldDrug
{
    public int SalesId { get; set; }

    public string? PharmacyName { get; set; }

    public string? DrugTradeName { get; set; }

    public decimal? DrugPrice { get; set; }

    public virtual Drug? DrugTradeNameNavigation { get; set; }

    public virtual Pharmacy? PharmacyNameNavigation { get; set; }
}

using System;
using System.Collections.Generic;

namespace Lab2;

public partial class Outgoing
{
    public int Id { get; set; }

    public int MedicineNameId { get; set; }

    public DateTime ImplementationDate { get; set; }

    public int Count { get; set; }

    public double SellingPrice { get; set; }

    public virtual Medicine MedicineName { get; set; } = null!;
}

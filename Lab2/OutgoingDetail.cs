using System;
using System.Collections.Generic;

namespace Lab2;

public partial class OutgoingDetail
{
    public int OutgoingId { get; set; }

    public string MedicineName { get; set; } = null!;

    public string? ProducerName { get; set; }

    public DateTime ImplementationDate { get; set; }

    public int OutgoingCount { get; set; }

    public double SellingPrice { get; set; }
}

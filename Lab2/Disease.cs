using System;
using System.Collections.Generic;

namespace Lab2;

public partial class Disease
{
    public int Id { get; set; }

    public int InternationalCode { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MedicinesForDisease> MedicinesForDiseases { get; set; } = new List<MedicinesForDisease>();
}

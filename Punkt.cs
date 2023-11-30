using System;
using System.Collections.Generic;

namespace storage;

public partial class Punkt
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}

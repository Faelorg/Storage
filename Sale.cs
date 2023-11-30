using System;
using System.Collections.Generic;

namespace storage;

public partial class Sale
{
    public int Id { get; set; }

    public double Price { get; set; }

    public DateTime DateAdd { get; set; }

    public int IdPunkt { get; set; }

    public virtual Punkt IdPunktNavigation { get; set; } = null!;

    public virtual ICollection<ProductSale> ProductSales { get; set; } = new List<ProductSale>();
}

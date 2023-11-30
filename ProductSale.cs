using System;
using System.Collections.Generic;

namespace storage;

public partial class ProductSale
{
    public int Id { get; set; }

    public int IdProduct { get; set; }

    public int IdSale { get; set; }

    public int Count { get; set; }

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual Sale IdSaleNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace storage;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Photo { get; set; } = null!;

    public int IdManufacturer { get; set; }

    public double Price { get; set; }

    public int? Discount { get; set; }

    public int Count { get; set; }

    public virtual Manufacturer IdManufacturerNavigation { get; set; } = null!;

    public virtual ICollection<ProductSale> ProductSales { get; set; } = new List<ProductSale>();
}

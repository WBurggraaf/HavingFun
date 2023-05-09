using System;
using System.Collections.Generic;

namespace Infrastructure.DB.AdventureWorks.Models;

public partial class ProductModel
{
    public long ProductModelId { get; set; }

    public string Name { get; set; } = null!;

    public string? CatalogDescription { get; set; }

    public byte[] Rowguid { get; set; } = null!;

    public byte[] ModifiedDate { get; set; } = null!;
}

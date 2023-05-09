using System;
using System.Collections.Generic;

namespace Infrastructure.DB.AdventureWorks.Models;

public partial class ProductModelProductDescription
{
    public long ProductModelId { get; set; }

    public long ProductDescriptionId { get; set; }

    public string Culture { get; set; } = null!;

    public byte[] Rowguid { get; set; } = null!;

    public byte[] ModifiedDate { get; set; } = null!;
}

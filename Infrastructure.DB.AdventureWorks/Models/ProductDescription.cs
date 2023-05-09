using System;
using System.Collections.Generic;

namespace Infrastructure.DB.AdventureWorks.Models;

public partial class ProductDescription
{
    public long ProductDescriptionId { get; set; }

    public string Description { get; set; } = null!;

    public byte[] Rowguid { get; set; } = null!;

    public byte[] ModifiedDate { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace Infrastructure.DB.AdventureWorks.Models;

public partial class ProductCategory
{
    public long ProductCategoryId { get; set; }

    public long? ParentProductCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public byte[] Rowguid { get; set; } = null!;

    public byte[] ModifiedDate { get; set; } = null!;
}

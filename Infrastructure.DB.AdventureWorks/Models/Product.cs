using System;
using System.Collections.Generic;

namespace Infrastructure.DB.AdventureWorks.Models;

public partial class Product
{
    public long ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string ProductNumber { get; set; } = null!;

    public string? Color { get; set; }

    public byte[] StandardCost { get; set; } = null!;

    public byte[] ListPrice { get; set; } = null!;

    public string? Size { get; set; }

    public byte[]? Weight { get; set; }

    public long? ProductCategoryId { get; set; }

    public long? ProductModelId { get; set; }

    public byte[] SellStartDate { get; set; } = null!;

    public byte[]? SellEndDate { get; set; }

    public byte[]? DiscontinuedDate { get; set; }

    public byte[]? ThumbNailPhoto { get; set; }

    public string? ThumbnailPhotoFileName { get; set; }

    public byte[] Rowguid { get; set; } = null!;

    public byte[] ModifiedDate { get; set; } = null!;
}

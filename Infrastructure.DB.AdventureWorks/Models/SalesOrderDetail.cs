using System;
using System.Collections.Generic;

namespace Infrastructure.DB.AdventureWorks.Models;

public partial class SalesOrderDetail
{
    public long SalesOrderId { get; set; }

    public long SalesOrderDetailId { get; set; }

    public long OrderQty { get; set; }

    public long ProductId { get; set; }

    public byte[] UnitPrice { get; set; } = null!;

    public byte[] UnitPriceDiscount { get; set; } = null!;

    public byte[] LineTotal { get; set; } = null!;

    public byte[] Rowguid { get; set; } = null!;

    public byte[] ModifiedDate { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace Infrastructure.DB.AdventureWorks.Models;

public partial class SalesOrderHeader
{
    public long SalesOrderId { get; set; }

    public long RevisionNumber { get; set; }

    public byte[] OrderDate { get; set; } = null!;

    public byte[] DueDate { get; set; } = null!;

    public byte[]? ShipDate { get; set; }

    public long Status { get; set; }

    public byte[] OnlineOrderFlag { get; set; } = null!;

    public string SalesOrderNumber { get; set; } = null!;

    public string? PurchaseOrderNumber { get; set; }

    public string? AccountNumber { get; set; }

    public long CustomerId { get; set; }

    public long? ShipToAddressId { get; set; }

    public long? BillToAddressId { get; set; }

    public string ShipMethod { get; set; } = null!;

    public string? CreditCardApprovalCode { get; set; }

    public byte[] SubTotal { get; set; } = null!;

    public byte[] TaxAmt { get; set; } = null!;

    public byte[] Freight { get; set; } = null!;

    public byte[] TotalDue { get; set; } = null!;

    public string? Comment { get; set; }

    public byte[] Rowguid { get; set; } = null!;

    public byte[] ModifiedDate { get; set; } = null!;
}

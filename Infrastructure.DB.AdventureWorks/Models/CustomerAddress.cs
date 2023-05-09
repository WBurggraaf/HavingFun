using System;
using System.Collections.Generic;

namespace Infrastructure.DB.AdventureWorks.Models;

public partial class CustomerAddress
{
    public long CustomerId { get; set; }

    public long AddressId { get; set; }

    public string AddressType { get; set; } = null!;

    public byte[] Rowguid { get; set; } = null!;

    public byte[] ModifiedDate { get; set; } = null!;
}

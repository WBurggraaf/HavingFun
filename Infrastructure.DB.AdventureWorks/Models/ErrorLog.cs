using System;
using System.Collections.Generic;

namespace Infrastructure.DB.AdventureWorks.Models;

public partial class ErrorLog
{
    public long ErrorLogId { get; set; }

    public byte[] ErrorTime { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public long ErrorNumber { get; set; }

    public long? ErrorSeverity { get; set; }

    public long? ErrorState { get; set; }

    public string? ErrorProcedure { get; set; }

    public long? ErrorLine { get; set; }

    public string ErrorMessage { get; set; } = null!;
}

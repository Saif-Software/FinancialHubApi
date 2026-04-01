using System;
using System.Collections.Generic;

namespace FinancialsHubWebAPI.Models;

public partial class TblMedium
{
    public long Id { get; set; }

    public string? TableName { get; set; }

    public long? TableId { get; set; }

    public string? FilePath { get; set; }
}

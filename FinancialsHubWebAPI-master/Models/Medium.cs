using System;
using System.Collections.Generic;

namespace FinancialsHubWebAPI.Models;

public partial class Medium
{
    public long Id { get; set; }

    public string? FilePath { get; set; }

    public string? RelatedTable { get; set; }

    public long? RelatedId { get; set; }

    public DateTime? UploadedAt { get; set; }
}

using System;
using System.Collections.Generic;

namespace FinancialsHubWebAPI.Models;

public partial class TransactionReport
{
    public long Id { get; set; }

    public string? ReportName { get; set; }

    public string? Notes { get; set; }

    public long? CreatorAccountId { get; set; }

    public long? CategoryId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Account? CreatorAccount { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<TransactionRecord> TransactionRecords { get; set; } = new List<TransactionRecord>();
}

using System;
using System.Collections.Generic;

namespace FinancialsHubWebAPI.Models;

public partial class TransactionRecord
{
    public long Id { get; set; }

    public DateOnly? TransactionDate { get; set; }

    public long? TransactionReportId { get; set; }

    public long? CategoryId { get; set; }

    public decimal? Amount { get; set; }

    public string? Description { get; set; }

    public virtual TransactionReport? TransactionReport { get; set; }

    public virtual Category? Category { get; set; }
}

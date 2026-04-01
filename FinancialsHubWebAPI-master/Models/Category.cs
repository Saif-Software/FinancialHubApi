using System;
using System.Collections.Generic;

namespace FinancialsHubWebAPI.Models;

public partial class Category
{
    public long Id { get; set; }

    public int? AmountMultiplier { get; set; }

    public string? CategoryName { get; set; }

    public int? OrderNumber { get; set; }

    public long? StatusId { get; set; }

    public virtual Status? Status { get; set; }

    public virtual ICollection<TransactionRecord> TransactionRecords { get; set; } = new List<TransactionRecord>();
}

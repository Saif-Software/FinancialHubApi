using System;
using System.Collections.Generic;

namespace FinancialsHubWebAPI.Models;

public enum ReportStatus
{
    Draft = 0,
    UnderReview = 1,
    Approved = 2,
    Rejected = 3
}

public class TransactionReport
{
    public long Id { get; set; }
    public string ReportName { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public long CreatorAccountId { get; set; }
    public long? CategoryId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Status workflow: Draft → UnderReview → Approved/Rejected
    public ReportStatus Status { get; set; } = ReportStatus.Draft;
    public string? RejectionReason { get; set; }
    public DateTime? StatusChangedAt { get; set; }
    public long? ReviewedByAccountId { get; set; }

    // Navigation
    public Account? CreatorAccount { get; set; }
    public Account? ReviewedByAccount { get; set; }
    public Category? Category { get; set; }
    public ICollection<TransactionRecord> TransactionRecords { get; set; } = new List<TransactionRecord>();
}

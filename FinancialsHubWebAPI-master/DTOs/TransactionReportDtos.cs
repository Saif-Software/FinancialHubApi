namespace FinancialsHubWebAPI.DTOs
{
    // ══════════════════════════════════════════════════════════════
    // ── Transaction Report DTOs ──────────────────────────────────
    // ══════════════════════════════════════════════════════════════

    public class CreateTransactionReportDto
    {
        public string ReportName { get; set; } = null!;
        public string? Notes { get; set; }
        public long CreatorAccountId { get; set; }
        public long? CategoryId { get; set; }
    }

    public class UpdateTransactionReportDto
    {
        public string? ReportName { get; set; }
        public string? Notes { get; set; }
        public long? CategoryId { get; set; }
    }

    // ── Response DTOs ────────────────────────────────────────────

    public class TransactionReportResponseDto
    {
        public long Id { get; set; }
        public string? ReportName { get; set; }
        public string? Notes { get; set; }
        public string? CreatorNameEn { get; set; }
        public string? CreatorNameAr { get; set; }
        public long? CreatorAccountId { get; set; }
        public string? CategoryName { get; set; }
        public long? CategoryId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<TransactionRecordResponseDto> TransactionRecords { get; set; } = new();
        public List<MediaResponseDto> Attachments { get; set; } = new();
    }

    public class TransactionReportListItemDto
    {
        public long Id { get; set; }
        public string? ReportName { get; set; }
        public string? Notes { get; set; }
        public string? CreatorNameEn { get; set; }
        public string? CreatorNameAr { get; set; }
        public long? CreatorAccountId { get; set; }
        public string? CategoryName { get; set; }
        public long? CategoryId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateOnly? LastTransactionDate { get; set; }
    }

    // ══════════════════════════════════════════════════════════════
    // ── Transaction Record DTOs ──────────────────────────────────
    // ══════════════════════════════════════════════════════════════

    public class CreateTransactionRecordDto
    {
        public DateOnly? TransactionDate { get; set; }
        public long TransactionReportId { get; set; }
        public long? CategoryId { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateTransactionRecordDto
    {
        public DateOnly? TransactionDate { get; set; }
        public long? CategoryId { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }
    }

    public class TransactionRecordResponseDto
    {
        public long Id { get; set; }
        public DateOnly? TransactionDate { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public long? CategoryId { get; set; }
        public long? TransactionReportId { get; set; }
        public List<MediaResponseDto> Attachments { get; set; } = new();
    }

    // ══════════════════════════════════════════════════════════════
    // ── Media DTO ────────────────────────────────────────────────
    // ══════════════════════════════════════════════════════════════

    public class MediaResponseDto
    {
        public long Id { get; set; }
        public string? FilePath { get; set; }
        public DateTime? UploadedAt { get; set; }
    }
}

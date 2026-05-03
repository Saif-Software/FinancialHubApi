namespace FinancialsHubWebAPI.DTOs
{
    // ══════════════════════════════════════
    // AUTH DTOs
    // ══════════════════════════════════════

    public class RegisterDto
    {
        public string FullNameEn { get; set; } = string.Empty;
        public string FullNameAr { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // "Admin" or "User"
    }

    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class AuthResponseDto
    {
        public long Id { get; set; }
        public string FullNameEn { get; set; } = string.Empty;
        public string FullNameAr { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }

    // ══════════════════════════════════════
    // NOTIFICATION DTOs
    // ══════════════════════════════════════

    public class NotificationResponseDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // "Info", "Warning", "Success", "Error"
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public long? RelatedReportId { get; set; }
    }

    public class MarkNotificationReadDto
    {
        public List<long> NotificationIds { get; set; } = new();
    }

    // ══════════════════════════════════════
    // REPORT STATUS DTOs
    // ══════════════════════════════════════

    public class SubmitForReviewDto
    {
        // Optionally add a note when submitting
        public string? SubmissionNote { get; set; }
    }

    public class ApproveReportDto
    {
        public string? ApprovalNote { get; set; }
    }

    public class RejectReportDto
    {
        public string RejectionReason { get; set; } = string.Empty;
    }

    // ══════════════════════════════════════
    // TRANSACTION REPORT DTOs (updated)
    // ══════════════════════════════════════

    public class TransactionReportListItemDto
    {
        public long Id { get; set; }
        public string ReportName { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public string? CreatorNameEn { get; set; }
        public string? CreatorNameAr { get; set; }
        public long CreatorAccountId { get; set; }
        public string? CategoryName { get; set; }
        public long? CategoryId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? LastTransactionDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class TransactionReportResponseDto
    {
        public long Id { get; set; }
        public string ReportName { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public string? CreatorNameEn { get; set; }
        public string? CreatorNameAr { get; set; }
        public long CreatorAccountId { get; set; }
        public string? CategoryName { get; set; }
        public long? CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? RejectionReason { get; set; }
        public DateTime? StatusChangedAt { get; set; }
        public string? ReviewedByNameEn { get; set; }
        public List<MediaResponseDto> Attachments { get; set; } = new();
        public List<TransactionRecordResponseDto> TransactionRecords { get; set; } = new();
    }

    public class CreateTransactionReportDto
    {
        public string ReportName { get; set; } = string.Empty;
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

    // ══════════════════════════════════════
    // TRANSACTION RECORD DTOs
    // ══════════════════════════════════════

    public class TransactionRecordResponseDto
    {
        public long Id { get; set; }
        public DateTime? TransactionDate { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public long? CategoryId { get; set; }
        public long? TransactionReportId { get; set; }
        public List<MediaResponseDto> Attachments { get; set; } = new();
    }

    public class CreateTransactionRecordDto
    {
        public DateTime? TransactionDate { get; set; }
        public long? CategoryId { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateTransactionRecordDto
    {
        public DateTime? TransactionDate { get; set; }
        public long? CategoryId { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }
    }

    // ══════════════════════════════════════
    // MEDIA DTO
    // ══════════════════════════════════════

    public class MediaResponseDto
    {
        public long Id { get; set; }
        public string? FilePath { get; set; }
        public DateTime? UploadedAt { get; set; }
    }
}
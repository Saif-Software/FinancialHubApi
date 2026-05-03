namespace FinancialsHubWebAPI.Models
{
  
    public enum NotificationType
    {
        Info,
        Warning,
        Success,
        Error
    }

    public class NotificationFinance
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public NotificationType Type { get; set; } = NotificationType.Info;
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Optional: link to a report
        public long? RelatedReportId { get; set; }

        // Navigation
        public Account? Account { get; set; }
        public TransactionReport? RelatedReport { get; set; }
    }
}

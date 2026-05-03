namespace FinancialsHubWebAPI.Models
{
    public class TransictionAccount
    {
        public long Id { get; set; }
        public string FullNameEn { get; set; } = string.Empty;
        public string FullNameAr { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // "Admin" or "User"
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<TransactionReport> TransactionReports { get; set; } = new List<TransactionReport>();
        public ICollection<NotificationFinance> Notifications { get; set; } = new List<NotificationFinance>();
    }
}

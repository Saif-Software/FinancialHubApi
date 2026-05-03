using FinancialsHubWebAPI.DTOs;
using FinancialsHubWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinancialsHubWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotificationController(AppDbContext context)
        {
            _context = context;
        }

        // ── GET: api/Notification ────────────────────────────────
        // Get all notifications for the logged-in user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationResponseDto>>> GetMyNotifications()
        {
            var accountId = GetCurrentAccountId();
            if (accountId == null) return Unauthorized();

            var notifications = await _context.NotificationFinancess
                .Where(n => n.AccountId == accountId.Value)
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new NotificationResponseDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Message = n.Message,
                    Type = n.Type.ToString(),
                    IsRead = n.IsRead,
                    CreatedAt = n.CreatedAt,
                    RelatedReportId = n.RelatedReportId
                })
                .ToListAsync();

            return Ok(notifications);
        }

        // ── GET: api/Notification/unread-count ───────────────────
        [HttpGet("unread-count")]
        public async Task<ActionResult> GetUnreadCount()
        {
            var accountId = GetCurrentAccountId();
            if (accountId == null) return Unauthorized();

            var count = await _context.NotificationFinancess
                .CountAsync(n => n.AccountId == accountId.Value && !n.IsRead);

            return Ok(new { unreadCount = count });
        }

        // ── GET: api/Notification/filter ─────────────────────────
        // Filter: ?type=approvals|alerts|unread|all
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<NotificationResponseDto>>> FilterNotifications(
            [FromQuery] string type = "all")
        {
            var accountId = GetCurrentAccountId();
            if (accountId == null) return Unauthorized();

            var query = _context.NotificationFinancess
                .Where(n => n.AccountId == accountId.Value);

            query = type switch
            {
                "approvals" => query.Where(n => n.Type == NotificationType.Success),
                "alerts" => query.Where(n => n.Type == NotificationType.Warning || n.Type == NotificationType.Error),
                "unread" => query.Where(n => !n.IsRead),
                _ => query
            };

            var result = await query
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new NotificationResponseDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Message = n.Message,
                    Type = n.Type.ToString(),
                    IsRead = n.IsRead,
                    CreatedAt = n.CreatedAt,
                    RelatedReportId = n.RelatedReportId
                })
                .ToListAsync();

            return Ok(result);
        }

        // ── PUT: api/Notification/{id}/read ──────────────────────
        [HttpPut("{id}/read")]
        public async Task<IActionResult> MarkAsRead(long id)
        {
            var accountId = GetCurrentAccountId();
            if (accountId == null) return Unauthorized();

            var notification = await _context.NotificationFinancess
                .FirstOrDefaultAsync(n => n.Id == id && n.AccountId == accountId.Value);

            if (notification == null)
                return NotFound(new { message = $"الإشعار برقم {id} غير موجود." });

            notification.IsRead = true;
            await _context.SaveChangesAsync();

            return Ok(new { message = "تم تعيين الإشعار كمقروء." });
        }

        // ── PUT: api/Notification/mark-all-read ──────────────────
        [HttpPut("mark-all-read")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var accountId = GetCurrentAccountId();
            if (accountId == null) return Unauthorized();

            var unread = await _context.NotificationFinancess
                .Where(n => n.AccountId == accountId.Value && !n.IsRead)
                .ToListAsync();

            unread.ForEach(n => n.IsRead = true);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"تم تعيين {unread.Count} إشعار كمقروء." });
        }

        // ── DELETE: api/Notification/{id} ────────────────────────
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var accountId = GetCurrentAccountId();
            if (accountId == null) return Unauthorized();

            var notification = await _context.NotificationFinancess
                .FirstOrDefaultAsync(n => n.Id == id && n.AccountId == accountId.Value);

            if (notification == null)
                return NotFound(new { message = $"الإشعار برقم {id} غير موجود." });

            _context.NotificationFinancess.Remove(notification);
            await _context.SaveChangesAsync();

            return Ok(new { message = "تم حذف الإشعار بنجاح." });
        }

        // ── DELETE: api/Notification/all ─────────────────────────
        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAll()
        {
            var accountId = GetCurrentAccountId();
            if (accountId == null) return Unauthorized();

            var notifications = await _context.NotificationFinancess
                .Where(n => n.AccountId == accountId.Value)
                .ToListAsync();

            _context.NotificationFinancess.RemoveRange(notifications);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"تم حذف {notifications.Count} إشعار بنجاح." });
        }

        // ── INTERNAL: Create notification (used by other controllers)
        public static async Task CreateNotificationAsync(
            AppDbContext context,
            long accountId,
            string title,
            string message,
            NotificationType type,
            long? relatedReportId = null)
        {
            var notification = new NotificationFinance
            {
                AccountId = accountId,
                Title = title,
                Message = message,
                Type = type,
                RelatedReportId = relatedReportId,
                CreatedAt = DateTime.Now,
                IsRead = false
            };

            await context.NotificationFinancess.AddAsync(notification);
            await context.SaveChangesAsync();
        }

        // ══════════════════════════════════════════════════════════
        private long? GetCurrentAccountId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return claim != null ? long.Parse(claim) : null;
        }
    }
}
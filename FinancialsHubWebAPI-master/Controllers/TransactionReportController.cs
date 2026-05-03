using FinancialsHubWebAPI.DTOs;
using FinancialsHubWebAPI.Models;
using FinancialsHubWebAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinancialsHubWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionReportController : ControllerBase
    {
        private readonly ITransactionReportRepo _reportRepo;
        private readonly AppDbContext _context;

        public TransactionReportController(ITransactionReportRepo reportRepo, AppDbContext context)
        {
            _reportRepo = reportRepo;
            _context = context;
        }

        // ── GET: api/TransactionReport ──────────────────────────────
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionReportListItemDto>>> GetAll(
            [FromQuery] string? status = null,
            [FromQuery] long? categoryId = null,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null)
        {
            var query = _context.TransactionReports
                .Include(r => r.CreatorAccount)
                .Include(r => r.Category)
                .Include(r => r.TransactionRecords)
                .AsQueryable();

            // Filter by status
            if (!string.IsNullOrEmpty(status) && Enum.TryParse<ReportStatus>(status, true, out var parsedStatus))
                query = query.Where(r => r.Status == parsedStatus);

            // Filter by category
            if (categoryId.HasValue)
                query = query.Where(r => r.CategoryId == categoryId);

            // Filter by date range (based on CreatedAt)
            if (fromDate.HasValue)
                query = query.Where(r => r.CreatedAt >= fromDate.Value);
            if (toDate.HasValue)
                query = query.Where(r => r.CreatedAt <= toDate.Value);

            var reports = await query.ToListAsync();

            var result = reports.Select(r => new TransactionReportListItemDto
            {
                Id = r.Id,
                ReportName = r.ReportName,
                Notes = r.Notes,
                CreatorNameEn = r.CreatorAccount?.FullNameEn,
                CreatorNameAr = r.CreatorAccount?.FullNameAr,
                CreatorAccountId = r.CreatorAccountId,
                CategoryName = r.Category?.CategoryName,
                CategoryId = r.CategoryId,
                TotalAmount = r.TransactionRecords.Sum(tr => tr.Amount ?? 0),
                LastTransactionDate = r.TransactionRecords
                    .Where(tr => tr.TransactionDate != null)
                    .OrderByDescending(tr => tr.TransactionDate)
                    .Select(tr => tr.TransactionDate)
                    .FirstOrDefault(),
                Status = r.Status.ToString(),
                CreatedAt = r.CreatedAt
            }).ToList();

            return Ok(result);
        }

        // ── GET: api/TransactionReport/{id} ─────────────────────
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionReportResponseDto>> GetById(long id)
        {
            var report = await _reportRepo.GetByIdWithDetailsAsync(id);

            if (report == null)
                return NotFound(new { message = $"التقرير برقم {id} غير موجود." });

            var recordIds = report.TransactionRecords.Select(tr => tr.Id).ToList();

            var allMedia = await _context.Media
                .Where(m =>
                    (m.RelatedTable == "TransactionReport" && m.RelatedId == id) ||
                    (m.RelatedTable == "TransactionRecord" && recordIds.Contains(m.RelatedId ?? 0)))
                .ToListAsync();

            var reportMedia = allMedia
                .Where(m => m.RelatedTable == "TransactionReport" && m.RelatedId == id)
                .Select(m => new MediaResponseDto
                {
                    Id = m.Id,
                    FilePath = m.FilePath,
                    UploadedAt = m.UploadedAt
                }).ToList();

            // Get reviewer name
            string? reviewerNameEn = null;
            if (report.ReviewedByAccountId.HasValue)
            {
                var reviewer = await _context.TransictionAccounts.FindAsync(report.ReviewedByAccountId.Value);
                reviewerNameEn = reviewer?.FullNameEn;
            }

            var result = new TransactionReportResponseDto
            {
                Id = report.Id,
                ReportName = report.ReportName,
                Notes = report.Notes,
                CreatorNameEn = report.CreatorAccount?.FullNameEn,
                CreatorNameAr = report.CreatorAccount?.FullNameAr,
                CreatorAccountId = report.CreatorAccountId,
                CategoryName = report.Category?.CategoryName,
                CategoryId = report.CategoryId,
                CreatedAt = report.CreatedAt,
                Status = report.Status.ToString(),
                RejectionReason = report.RejectionReason,
                StatusChangedAt = report.StatusChangedAt,
                ReviewedByNameEn = reviewerNameEn,
                TotalAmount = report.TransactionRecords.Sum(tr => tr.Amount ?? 0),
                Attachments = reportMedia,
                TransactionRecords = report.TransactionRecords.Select(tr => new TransactionRecordResponseDto
                {
                    Id = tr.Id,
                    TransactionDate = tr.TransactionDate,
                    Amount = tr.Amount,
                    Description = tr.Description,
                    CategoryName = tr.Category?.CategoryName,
                    CategoryId = tr.CategoryId,
                    TransactionReportId = tr.TransactionReportId,
                    Attachments = allMedia
                        .Where(m => m.RelatedTable == "TransactionRecord" && m.RelatedId == tr.Id)
                        .Select(m => new MediaResponseDto
                        {
                            Id = m.Id,
                            FilePath = m.FilePath,
                            UploadedAt = m.UploadedAt
                        }).ToList()
                }).ToList()
            };

            return Ok(result);
        }

        // ── POST: api/TransactionReport ─────────────────────────
        [HttpPost]
        public async Task<ActionResult<TransactionReportResponseDto>> Create([FromBody] CreateTransactionReportDto dto)
        {
            var accountExists = await _context.TransictionAccounts.AnyAsync(a => a.Id == dto.CreatorAccountId);
            if (!accountExists)
                return BadRequest(new { message = $"الحساب برقم {dto.CreatorAccountId} غير موجود." });

            if (dto.CategoryId.HasValue)
            {
                var categoryExists = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId.Value);
                if (!categoryExists)
                    return BadRequest(new { message = $"التصنيف برقم {dto.CategoryId} غير موجود." });
            }

            var report = new TransactionReport
            {
                ReportName = dto.ReportName,
                Notes = dto.Notes,
                CreatorAccountId = dto.CreatorAccountId,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.Now,
                Status = ReportStatus.Draft
            };

            await _reportRepo.CreateAsync(report);

            // Send notification to creator
            await NotificationController.CreateNotificationAsync(
                _context,
                dto.CreatorAccountId,
                "تم إنشاء تقرير جديد",
                $"تم إنشاء تقرير '{dto.ReportName}' بنجاح.",
                NotificationType.Success,
                report.Id
            );

            var created = await _reportRepo.GetByIdWithDetailsAsync(report.Id);

            return CreatedAtAction(nameof(GetById), new { id = report.Id }, new TransactionReportResponseDto
            {
                Id = created!.Id,
                ReportName = created.ReportName,
                Notes = created.Notes,
                CreatorNameEn = created.CreatorAccount?.FullNameEn,
                CreatorNameAr = created.CreatorAccount?.FullNameAr,
                CreatorAccountId = created.CreatorAccountId,
                CategoryName = created.Category?.CategoryName,
                CategoryId = created.CategoryId,
                CreatedAt = created.CreatedAt,
                Status = created.Status.ToString(),
                TotalAmount = 0,
                TransactionRecords = new(),
                Attachments = new()
            });
        }

        // ── PUT: api/TransactionReport/{id} ─────────────────────
        [HttpPut("{id}")]
        public async Task<ActionResult<TransactionReportResponseDto>> Update(long id, [FromBody] UpdateTransactionReportDto dto)
        {
            var report = await _context.TransactionReports.FindAsync(id);
            if (report == null)
                return NotFound(new { message = $"التقرير برقم {id} غير موجود." });

            // Only allow editing if Draft or Rejected
            if (report.Status != ReportStatus.Draft && report.Status != ReportStatus.Rejected)
                return BadRequest(new { message = "لا يمكن تعديل التقرير إلا في حالة مسودة أو مرفوض." });

            if (dto.ReportName != null) report.ReportName = dto.ReportName;
            if (dto.Notes != null) report.Notes = dto.Notes;
            if (dto.CategoryId.HasValue)
            {
                var categoryExists = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId.Value);
                if (!categoryExists)
                    return BadRequest(new { message = $"التصنيف برقم {dto.CategoryId} غير موجود." });
                report.CategoryId = dto.CategoryId;
            }

            await _context.SaveChangesAsync();

            var updated = await _reportRepo.GetByIdWithDetailsAsync(id);
            return Ok(await BuildReportResponseDto(updated!));
        }

        // ── DELETE: api/TransactionReport/{id} ──────────────────
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _reportRepo.DeleteWithRecordsAsync(id);
                return Ok(new { message = $"تم حذف التقرير برقم {id} وجميع السجلات المرتبطة به بنجاح." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // ══════════════════════════════════════════════════════════
        // ── STATUS / APPROVAL ENDPOINTS ──────────────────────────
        // ══════════════════════════════════════════════════════════

        // ── POST: api/TransactionReport/{id}/submit ──────────────
        // Creator submits for review: Draft → UnderReview
        [HttpPost("{id}/submit")]
        public async Task<ActionResult> SubmitForReview(long id, [FromBody] SubmitForReviewDto? dto)
        {
            var report = await _context.TransactionReports
                .Include(r => r.CreatorAccount)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (report == null)
                return NotFound(new { message = $"التقرير برقم {id} غير موجود." });

            if (report.Status != ReportStatus.Draft)
                return BadRequest(new { message = "يمكن إرسال التقرير للمراجعة فقط إذا كان في حالة مسودة." });

            report.Status = ReportStatus.UnderReview;
            report.StatusChangedAt = DateTime.Now;
            report.RejectionReason = null;
            await _context.SaveChangesAsync();

            // Notify all Admins
            var admins = await _context.TransictionAccounts
                .Where(a => a.Role == "Admin" && a.IsActive)
                .ToListAsync();

            foreach (var admin in admins)
            {
                await NotificationController.CreateNotificationAsync(
                    _context,
                    admin.Id,
                    "تقرير بانتظار المراجعة",
                    $"التقرير '{report.ReportName}' بقلم {report.CreatorAccount?.FullNameAr} يحتاج إلى مراجعة.",
                    NotificationType.Info,
                    report.Id
                );
            }

            return Ok(new { message = "تم إرسال التقرير للمراجعة بنجاح.", status = "UnderReview" });
        }

        // ── POST: api/TransactionReport/{id}/approve ─────────────
        // Admin approves: UnderReview → Approved
        [HttpPost("{id}/approve")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Approve(long id, [FromBody] ApproveReportDto? dto)
        {
            var report = await _context.TransactionReports.FindAsync(id);
            if (report == null)
                return NotFound(new { message = $"التقرير برقم {id} غير موجود." });

            if (report.Status != ReportStatus.UnderReview)
                return BadRequest(new { message = "يمكن اعتماد التقارير التي في حالة قيد المراجعة فقط." });

            var reviewerId = GetCurrentAccountId();
            report.Status = ReportStatus.Approved;
            report.StatusChangedAt = DateTime.Now;
            report.ReviewedByAccountId = reviewerId;
            report.RejectionReason = null;
            await _context.SaveChangesAsync();

            // Notify creator
            await NotificationController.CreateNotificationAsync(
                _context,
                report.CreatorAccountId,
                "تم الموافقة على التقرير",
                $"تم الموافقة على تقرير '{report.ReportName}' بنجاح.",
                NotificationType.Success,
                report.Id
            );

            return Ok(new { message = "تم اعتماد التقرير بنجاح.", status = "Approved" });
        }

        // ── POST: api/TransactionReport/{id}/reject ──────────────
        // Admin rejects: UnderReview → Rejected
        [HttpPost("{id}/reject")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Reject(long id, [FromBody] RejectReportDto dto)
        {
            var report = await _context.TransactionReports.FindAsync(id);
            if (report == null)
                return NotFound(new { message = $"التقرير برقم {id} غير موجود." });

            if (report.Status != ReportStatus.UnderReview)
                return BadRequest(new { message = "يمكن رفض التقارير التي في حالة قيد المراجعة فقط." });

            if (string.IsNullOrWhiteSpace(dto.RejectionReason))
                return BadRequest(new { message = "يجب إدخال سبب الرفض." });

            var reviewerId = GetCurrentAccountId();
            report.Status = ReportStatus.Rejected;
            report.StatusChangedAt = DateTime.Now;
            report.ReviewedByAccountId = reviewerId;
            report.RejectionReason = dto.RejectionReason;
            await _context.SaveChangesAsync();

            // Notify creator
            await NotificationController.CreateNotificationAsync(
                _context,
                report.CreatorAccountId,
                "تم رفض التقرير",
                $"تم رفض تقرير '{report.ReportName}'. السبب: {dto.RejectionReason}",
                NotificationType.Error,
                report.Id
            );

            return Ok(new { message = "تم رفض التقرير.", status = "Rejected" });
        }

        // ── POST: api/TransactionReport/{id}/revert-to-draft ─────
        // Creator reverts rejected report back to draft for editing
        [HttpPost("{id}/revert-to-draft")]
        public async Task<ActionResult> RevertToDraft(long id)
        {
            var report = await _context.TransactionReports.FindAsync(id);
            if (report == null)
                return NotFound(new { message = $"التقرير برقم {id} غير موجود." });

            if (report.Status != ReportStatus.Rejected)
                return BadRequest(new { message = "يمكن إعادة التقرير إلى المسودة فقط إذا كان مرفوضاً." });

            report.Status = ReportStatus.Draft;
            report.StatusChangedAt = DateTime.Now;
            report.RejectionReason = null;
            report.ReviewedByAccountId = null;
            await _context.SaveChangesAsync();

            return Ok(new { message = "تم إعادة التقرير إلى المسودة.", status = "Draft" });
        }

        // ══════════════════════════════════════════════════════════
        // ── TRANSACTION RECORD ENDPOINTS ─────────────────────────
        // ══════════════════════════════════════════════════════════

        // ── GET: api/TransactionReport/{reportId}/records ───────
        [HttpGet("{reportId}/records")]
        public async Task<ActionResult<IEnumerable<TransactionRecordResponseDto>>> GetRecords(long reportId)
        {
            var reportExists = await _context.TransactionReports.AnyAsync(r => r.Id == reportId);
            if (!reportExists)
                return NotFound(new { message = $"التقرير برقم {reportId} غير موجود." });

            var records = await _context.TransactionRecords
                .Where(r => r.TransactionReportId == reportId)
                .Include(r => r.Category)
                .ToListAsync();

            var recordIds = records.Select(r => r.Id).ToList();
            var media = await _context.Media
                .Where(m => m.RelatedTable == "TransactionRecord" && recordIds.Contains(m.RelatedId ?? 0))
                .ToListAsync();

            var result = records.Select(tr => new TransactionRecordResponseDto
            {
                Id = tr.Id,
                TransactionDate = tr.TransactionDate,
                Amount = tr.Amount,
                Description = tr.Description,
                CategoryName = tr.Category?.CategoryName,
                CategoryId = tr.CategoryId,
                TransactionReportId = tr.TransactionReportId,
                Attachments = media
                    .Where(m => m.RelatedId == tr.Id)
                    .Select(m => new MediaResponseDto
                    {
                        Id = m.Id,
                        FilePath = m.FilePath,
                        UploadedAt = m.UploadedAt
                    }).ToList()
            }).ToList();

            return Ok(result);
        }

        // ── POST: api/TransactionReport/{reportId}/records ──────
        [HttpPost("{reportId}/records")]
        public async Task<ActionResult<TransactionRecordResponseDto>> AddRecord(long reportId, [FromBody] CreateTransactionRecordDto dto)
        {
            var report = await _context.TransactionReports.FindAsync(reportId);
            if (report == null)
                return NotFound(new { message = $"التقرير برقم {reportId} غير موجود." });

            // Only allow adding records to Draft or Rejected reports
            if (report.Status != ReportStatus.Draft && report.Status != ReportStatus.Rejected)
                return BadRequest(new { message = "لا يمكن إضافة سجلات إلى تقرير قيد المراجعة أو معتمد." });

            var record = new TransactionRecord
            {
                TransactionDate = dto.TransactionDate,
                TransactionReportId = reportId,
                CategoryId = dto.CategoryId,
                Amount = dto.Amount,
                Description = dto.Description
            };

            await _context.TransactionRecords.AddAsync(record);
            await _context.SaveChangesAsync();

            var created = await _context.TransactionRecords
                .Include(r => r.Category)
                .FirstOrDefaultAsync(r => r.Id == record.Id);

            return CreatedAtAction(nameof(GetRecords), new { reportId }, new TransactionRecordResponseDto
            {
                Id = created!.Id,
                TransactionDate = created.TransactionDate,
                Amount = created.Amount,
                Description = created.Description,
                CategoryName = created.Category?.CategoryName,
                CategoryId = created.CategoryId,
                TransactionReportId = created.TransactionReportId,
                Attachments = new()
            });
        }

        // ── PUT: api/TransactionReport/{reportId}/records/{recordId}
        [HttpPut("{reportId}/records/{recordId}")]
        public async Task<ActionResult<TransactionRecordResponseDto>> UpdateRecord(long reportId, long recordId, [FromBody] UpdateTransactionRecordDto dto)
        {
            var report = await _context.TransactionReports.FindAsync(reportId);
            if (report == null)
                return NotFound(new { message = $"التقرير برقم {reportId} غير موجود." });

            if (report.Status != ReportStatus.Draft && report.Status != ReportStatus.Rejected)
                return BadRequest(new { message = "لا يمكن تعديل سجلات تقرير قيد المراجعة أو معتمد." });

            var record = await _context.TransactionRecords
                .FirstOrDefaultAsync(r => r.Id == recordId && r.TransactionReportId == reportId);

            if (record == null)
                return NotFound(new { message = $"السجل برقم {recordId} غير موجود في التقرير {reportId}." });

            if (dto.TransactionDate.HasValue) record.TransactionDate = dto.TransactionDate;
            if (dto.CategoryId.HasValue) record.CategoryId = dto.CategoryId;
            if (dto.Amount.HasValue) record.Amount = dto.Amount;
            if (dto.Description != null) record.Description = dto.Description;

            await _context.SaveChangesAsync();

            var updated = await _context.TransactionRecords
                .Include(r => r.Category)
                .FirstOrDefaultAsync(r => r.Id == recordId);

            return Ok(new TransactionRecordResponseDto
            {
                Id = updated!.Id,
                TransactionDate = updated.TransactionDate,
                Amount = updated.Amount,
                Description = updated.Description,
                CategoryName = updated.Category?.CategoryName,
                CategoryId = updated.CategoryId,
                TransactionReportId = updated.TransactionReportId,
                Attachments = new()
            });
        }

        // ── DELETE: api/TransactionReport/{reportId}/records/{recordId}
        [HttpDelete("{reportId}/records/{recordId}")]
        public async Task<IActionResult> DeleteRecord(long reportId, long recordId)
        {
            var report = await _context.TransactionReports.FindAsync(reportId);
            if (report == null)
                return NotFound(new { message = $"التقرير برقم {reportId} غير موجود." });

            if (report.Status != ReportStatus.Draft && report.Status != ReportStatus.Rejected)
                return BadRequest(new { message = "لا يمكن حذف سجلات تقرير قيد المراجعة أو معتمد." });

            var record = await _context.TransactionRecords
                .FirstOrDefaultAsync(r => r.Id == recordId && r.TransactionReportId == reportId);

            if (record == null)
                return NotFound(new { message = $"السجل برقم {recordId} غير موجود في التقرير {reportId}." });

            var media = await _context.Media
                .Where(m => m.RelatedTable == "TransactionRecord" && m.RelatedId == recordId)
                .ToListAsync();
            _context.Media.RemoveRange(media);

            _context.TransactionRecords.Remove(record);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"تم حذف السجل برقم {recordId} بنجاح." });
        }

        // ══════════════════════════════════════════════════════════
        // ── ATTACHMENT ENDPOINTS ─────────────────────────────────
        // ══════════════════════════════════════════════════════════

        [HttpPost("{reportId}/records/{recordId}/attachments")]
        public async Task<ActionResult<MediaResponseDto>> UploadAttachment(long reportId, long recordId, IFormFile file)
        {
            var recordExists = await _context.TransactionRecords
                .AnyAsync(r => r.Id == recordId && r.TransactionReportId == reportId);

            if (!recordExists)
                return NotFound(new { message = $"السجل برقم {recordId} غير موجود في التقرير {reportId}." });

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var media = new Medium
            {
                FilePath = $"/uploads/{uniqueFileName}",
                RelatedTable = "TransactionRecord",
                RelatedId = recordId,
                UploadedAt = DateTime.Now
            };

            await _context.Media.AddAsync(media);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecords), new { reportId }, new MediaResponseDto
            {
                Id = media.Id,
                FilePath = media.FilePath,
                UploadedAt = media.UploadedAt
            });
        }

        [HttpDelete("attachments/{mediaId}")]
        public async Task<IActionResult> DeleteAttachment(long mediaId)
        {
            var media = await _context.Media.FindAsync(mediaId);
            if (media == null)
                return NotFound(new { message = $"المرفق برقم {mediaId} غير موجود." });

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", media.FilePath?.TrimStart('/') ?? "");
            if (System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);

            _context.Media.Remove(media);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"تم حذف المرفق برقم {mediaId} بنجاح." });
        }

        // ══════════════════════════════════════════════════════════
        // ── HELPER METHODS ───────────────────────────────────────
        // ══════════════════════════════════════════════════════════

        private long? GetCurrentAccountId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return claim != null ? long.Parse(claim) : null;
        }

        private async Task<TransactionReportResponseDto> BuildReportResponseDto(TransactionReport report)
        {
            var recordIds = report.TransactionRecords.Select(tr => tr.Id).ToList();

            var allMedia = await _context.Media
                .Where(m =>
                    (m.RelatedTable == "TransactionReport" && m.RelatedId == report.Id) ||
                    (m.RelatedTable == "TransactionRecord" && recordIds.Contains(m.RelatedId ?? 0)))
                .ToListAsync();

            string? reviewerNameEn = null;
            if (report.ReviewedByAccountId.HasValue)
            {
                var reviewer = await _context.TransictionAccounts.FindAsync(report.ReviewedByAccountId.Value);
                reviewerNameEn = reviewer?.FullNameEn;
            }

            return new TransactionReportResponseDto
            {
                Id = report.Id,
                ReportName = report.ReportName,
                Notes = report.Notes,
                CreatorNameEn = report.CreatorAccount?.FullNameEn,
                CreatorNameAr = report.CreatorAccount?.FullNameAr,
                CreatorAccountId = report.CreatorAccountId,
                CategoryName = report.Category?.CategoryName,
                CategoryId = report.CategoryId,
                CreatedAt = report.CreatedAt,
                Status = report.Status.ToString(),
                RejectionReason = report.RejectionReason,
                StatusChangedAt = report.StatusChangedAt,
                ReviewedByNameEn = reviewerNameEn,
                TotalAmount = report.TransactionRecords.Sum(tr => tr.Amount ?? 0),
                Attachments = allMedia
                    .Where(m => m.RelatedTable == "TransactionReport" && m.RelatedId == report.Id)
                    .Select(m => new MediaResponseDto { Id = m.Id, FilePath = m.FilePath, UploadedAt = m.UploadedAt }).ToList(),
                TransactionRecords = report.TransactionRecords.Select(tr => new TransactionRecordResponseDto
                {
                    Id = tr.Id,
                    TransactionDate = tr.TransactionDate,
                    Amount = tr.Amount,
                    Description = tr.Description,
                    CategoryName = tr.Category?.CategoryName,
                    CategoryId = tr.CategoryId,
                    TransactionReportId = tr.TransactionReportId,
                    Attachments = allMedia
                        .Where(m => m.RelatedTable == "TransactionRecord" && m.RelatedId == tr.Id)
                        .Select(m => new MediaResponseDto { Id = m.Id, FilePath = m.FilePath, UploadedAt = m.UploadedAt }).ToList()
                }).ToList()
            };
        }
    }
}
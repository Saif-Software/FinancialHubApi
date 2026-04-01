using FinancialsHubWebAPI.DTOs;
using FinancialsHubWebAPI.Models;
using FinancialsHubWebAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinancialsHubWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionReportController : ControllerBase
    {
        private readonly ITransactionReportRepo _reportRepo;
        private readonly AppDbContext _context;

        public TransactionReportController(ITransactionReportRepo reportRepo, AppDbContext context)
        {
            _reportRepo = reportRepo;
            _context = context;
        }
        // ── GET: api/TransactionReport ──────────────────────────
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionReportListItemDto>>> GetAll()
        {
            var reports = await _reportRepo.GetAllWithDetailsAsync();

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
                    .FirstOrDefault()
            }).ToList();

            return Ok(result);
        }

        // ── GET: api/TransactionReport/{id} ─────────────────────
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionReportResponseDto>> GetById(long id)
        {
            var report = await _reportRepo.GetByIdWithDetailsAsync(id);

            if (report == null)
                return NotFound(new { message = $"TransactionReport with Id {id} not found." });

            // Fetch media for this report and its records
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
            // Validate that the creator account exists
            var accountExists = await _context.Accounts.AnyAsync(a => a.Id == dto.CreatorAccountId);
            if (!accountExists)
                return BadRequest(new { message = $"Account with Id {dto.CreatorAccountId} not found." });

            // Validate category if provided
            if (dto.CategoryId.HasValue)
            {
                var categoryExists = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId.Value);
                if (!categoryExists)
                    return BadRequest(new { message = $"Category with Id {dto.CategoryId} not found." });
            }

            var report = new TransactionReport
            {
                ReportName = dto.ReportName,
                Notes = dto.Notes,
                CreatorAccountId = dto.CreatorAccountId,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.Now
            };

            await _reportRepo.CreateAsync(report);

            // Reload with details for the response
            var created = await _reportRepo.GetByIdWithDetailsAsync(report.Id);

            var result = new TransactionReportResponseDto
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
                TotalAmount = 0,
                TransactionRecords = new(),
                Attachments = new()
            };

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // ── PUT: api/TransactionReport/{id} ─────────────────────
        [HttpPut("{id}")]
        public async Task<ActionResult<TransactionReportResponseDto>> Update(long id, [FromBody] UpdateTransactionReportDto dto)
        {
            var report = await _context.TransactionReports.FindAsync(id);
            if (report == null)
                return NotFound(new { message = $"TransactionReport with Id {id} not found." });

            // Update only provided fields
            if (dto.ReportName != null) report.ReportName = dto.ReportName;
            if (dto.Notes != null) report.Notes = dto.Notes;
            if (dto.CategoryId.HasValue)
            {
                var categoryExists = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId.Value);
                if (!categoryExists)
                    return BadRequest(new { message = $"Category with Id {dto.CategoryId} not found." });
                report.CategoryId = dto.CategoryId;
            }

            await _context.SaveChangesAsync();

            // Return updated report with full details
            var updated = await _reportRepo.GetByIdWithDetailsAsync(id);
            return Ok(await BuildReportResponseDto(updated!));
        }

        // ── DELETE: api/TransactionReport/{id} ──────────────────
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _reportRepo.DeleteWithRecordsAsync(id);
                return Ok(new { message = $"TransactionReport with Id {id} and all related records deleted successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
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
                return NotFound(new { message = $"TransactionReport with Id {reportId} not found." });

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
            var reportExists = await _context.TransactionReports.AnyAsync(r => r.Id == reportId);
            if (!reportExists)
                return NotFound(new { message = $"TransactionReport with Id {reportId} not found." });

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

            // Reload with category
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
            var record = await _context.TransactionRecords
                .FirstOrDefaultAsync(r => r.Id == recordId && r.TransactionReportId == reportId);

            if (record == null)
                return NotFound(new { message = $"TransactionRecord with Id {recordId} not found in Report {reportId}." });

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
            var record = await _context.TransactionRecords
                .FirstOrDefaultAsync(r => r.Id == recordId && r.TransactionReportId == reportId);

            if (record == null)
                return NotFound(new { message = $"TransactionRecord with Id {recordId} not found in Report {reportId}." });

            // Delete related media
            var media = await _context.Media
                .Where(m => m.RelatedTable == "TransactionRecord" && m.RelatedId == recordId)
                .ToListAsync();
            _context.Media.RemoveRange(media);

            _context.TransactionRecords.Remove(record);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"TransactionRecord with Id {recordId} deleted successfully." });
        }

        // ══════════════════════════════════════════════════════════
        // ── ATTACHMENT ENDPOINTS ─────────────────────────────────
        // ══════════════════════════════════════════════════════════

        // ── POST: api/TransactionReport/{reportId}/records/{recordId}/attachments
        [HttpPost("{reportId}/records/{recordId}/attachments")]
        public async Task<ActionResult<MediaResponseDto>> UploadAttachment(long reportId, long recordId, IFormFile file)
        {
            var recordExists = await _context.TransactionRecords
                .AnyAsync(r => r.Id == recordId && r.TransactionReportId == reportId);

            if (!recordExists)
                return NotFound(new { message = $"TransactionRecord with Id {recordId} not found in Report {reportId}." });

            // Save file to uploads folder
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

        // ── DELETE: api/TransactionReport/attachments/{mediaId} ─
        [HttpDelete("attachments/{mediaId}")]
        public async Task<IActionResult> DeleteAttachment(long mediaId)
        {
            var media = await _context.Media.FindAsync(mediaId);
            if (media == null)
                return NotFound(new { message = $"Attachment with Id {mediaId} not found." });

            // Delete file from disk
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", media.FilePath?.TrimStart('/') ?? "");
            if (System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);

            _context.Media.Remove(media);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Attachment with Id {mediaId} deleted successfully." });
        }

        // ══════════════════════════════════════════════════════════
        // ── HELPER METHODS ───────────────────────────────────────
        // ══════════════════════════════════════════════════════════

        private async Task<TransactionReportResponseDto> BuildReportResponseDto(TransactionReport report)
        {
            var recordIds = report.TransactionRecords.Select(tr => tr.Id).ToList();

            var allMedia = await _context.Media
                .Where(m =>
                    (m.RelatedTable == "TransactionReport" && m.RelatedId == report.Id) ||
                    (m.RelatedTable == "TransactionRecord" && recordIds.Contains(m.RelatedId ?? 0)))
                .ToListAsync();

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

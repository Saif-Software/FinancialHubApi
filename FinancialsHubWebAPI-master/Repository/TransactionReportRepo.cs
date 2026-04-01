using FinancialsHubWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialsHubWebAPI.Repository
{
    public class TransactionReportRepo : GenericRepo<TransactionReport>, ITransactionReportRepo
    {
        private readonly AppDbContext _context;

        public TransactionReportRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransactionReport>> GetAllWithDetailsAsync()
        {
            return await _context.TransactionReports
                .Include(r => r.CreatorAccount)
                .Include(r => r.Category)
                .Include(r => r.TransactionRecords)
                    .ThenInclude(tr => tr.Category)
                .OrderByDescending(r => r.Id)
                .ToListAsync();
        }

        public async Task<TransactionReport?> GetByIdWithDetailsAsync(long id)
        {
            return await _context.TransactionReports
                .Include(r => r.CreatorAccount)
                .Include(r => r.Category)
                .Include(r => r.TransactionRecords)
                    .ThenInclude(tr => tr.Category)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task DeleteWithRecordsAsync(long id)
        {
            var report = await _context.TransactionReports
                .Include(r => r.TransactionRecords)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (report == null)
                throw new KeyNotFoundException($"TransactionReport with Id {id} not found.");

            // Delete related media (attachments) for each transaction record
            var recordIds = report.TransactionRecords.Select(tr => tr.Id).ToList();
            var relatedMedia = await _context.Media
                .Where(m => m.RelatedTable == "TransactionRecord" && recordIds.Contains(m.RelatedId ?? 0))
                .ToListAsync();
            _context.Media.RemoveRange(relatedMedia);

            // Delete related media for the report itself
            var reportMedia = await _context.Media
                .Where(m => m.RelatedTable == "TransactionReport" && m.RelatedId == id)
                .ToListAsync();
            _context.Media.RemoveRange(reportMedia);

            // Delete transaction records
            _context.TransactionRecords.RemoveRange(report.TransactionRecords);

            // Delete the report
            _context.TransactionReports.Remove(report);

            await _context.SaveChangesAsync();
        }
    }
}

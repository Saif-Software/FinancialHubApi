using FinancialsHubWebAPI.Models;

namespace FinancialsHubWebAPI.Repository
{
    public interface ITransactionReportRepo : IGenericRepo<TransactionReport>
    {
        Task<IEnumerable<TransactionReport>> GetAllWithDetailsAsync();
        Task<TransactionReport?> GetByIdWithDetailsAsync(long id);
        Task DeleteWithRecordsAsync(long id);
    }
}

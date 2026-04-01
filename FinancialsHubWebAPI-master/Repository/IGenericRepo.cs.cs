namespace FinancialsHubWebAPI.Repository
{
    public interface IGenericRepo<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task CreateAsync(T entity);
        public Task DeleteAsync(long Id);
        public Task UpdateAsync(T entity);
        public Task<T> GetById(long Id);
    }
}

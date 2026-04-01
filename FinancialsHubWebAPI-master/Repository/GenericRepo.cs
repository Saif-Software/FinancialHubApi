using FinancialsHubWebAPI;
using Microsoft.EntityFrameworkCore;

namespace FinancialsHubWebAPI.Repository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly AppDbContext _context;
        public GenericRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long Id)
        {
            var res = await _context.Set<T>().FindAsync(Id);
            if (res != null)
            {
                _context.Set<T>().Remove(res);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(long Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}

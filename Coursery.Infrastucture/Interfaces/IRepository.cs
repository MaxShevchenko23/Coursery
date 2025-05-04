using Coursery.Infrastucture.Data;

namespace Coursery.Infrastucture.Interfaces;

public interface IRepository<TEntity> : IDisposable
{
        CourseryDbContext _context { get; set; }
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);

        Task<int> SaveChangesAsync()
        { 
                return _context.SaveChangesAsync();
        }
}
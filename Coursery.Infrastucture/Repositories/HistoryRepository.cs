using Coursery.Domain.Entities;
    using Coursery.Infrastucture.Data;
    using Coursery.Infrastucture.Interfaces;
    using Microsoft.EntityFrameworkCore;
    
    namespace Coursery.Infrastucture.Repositories;
    
    public class HistoryRepository : IHistoryRepository
    {
       public CourseryDbContext _context { get; set; }
    
        public HistoryRepository(CourseryDbContext context)
        {
            _context = context;
        }
        public async Task<History> GetByIdAsync(int id)
        {
            return await _context.History.FindAsync(id);
        }
    
        public async Task<IEnumerable<History>> GetAllAsync()
        {
            return await _context.History.ToListAsync();
        }
    
        public async Task<History> AddAsync(History entity)
        {
            var record = await _context.History.FirstOrDefaultAsync(e=>e.UserId == entity.UserId && e.LessonId == entity.LessonId);

            if (record != null)
            {
                record.SetCreatedAt();
                
                _context.History.Update(record);
                await _context.SaveChangesAsync();
                return record;
            }
            
            entity.SetCreatedAt();
            
            await _context.History.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    
        public async Task UpdateAsync(History entity)
        {
            _context.History.Update(entity);
            await _context.SaveChangesAsync();
        }
    
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.History.FindAsync(id);
            if (entity != null)
            {
                _context.History.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task<IList<History>> GetHistoriesPaginated(int page, int pageSize, int userId)
        {
            var histories = 
                await _context.History
                    .Where(e => e.UserId == userId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            return histories;
        }
        
        public async Task<List<History>> GetHistoriesByUserId(int userId)
        {
            var histories = 
                await _context.History
                    .Where(e => e.UserId == userId)
                    .Include(e=>e.Lesson)
                    .OrderBy(e=>e.CreatedAt)
                    .ToListAsync();
            
            return histories;
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
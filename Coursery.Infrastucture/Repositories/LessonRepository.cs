using Coursery.Domain.Entities;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coursery.Infrastucture.Repositories;

public class LessonRepository : ILessonRepository
{
    public CourseryDbContext _context { get; set; }
    
    public LessonRepository(CourseryDbContext context)
    {
        _context = context;
    }

    public async Task<Lesson> GetByIdAsync(int id)
    {
        return await _context.Lessons.FindAsync(id);
    }

    public async Task<IEnumerable<Lesson>> GetAllAsync()
    {
        return await _context.Lessons.ToListAsync();
    }

    public async Task<Lesson> AddAsync(Lesson entity)
    {
        await _context.Lessons.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Lesson entity)
    {
        _context.Lessons.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Lessons.FindAsync(id);
        if (entity != null)
        {
            _context.Lessons.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<IList<Lesson>> GetLessonsPaginatedForUser(int page, int pageSize, int userId)
    {
        var courseIds = _context.Enrolls
            .Where(e=>e.UserId==userId)
            .Select(e=>e.CourseId);
        
        
        var lessons = 
            await _context.Lessons
                .Include(e=>e.Module)
                .ThenInclude(e=>e.Lessons)
                .Where(e => courseIds.Contains(e.Module.CourseId) )
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        
        return lessons;
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<List<Lesson>> GetLessonsByModuleId(int value)
    {
        
        var lessons = await _context.Lessons
            .Where(e => e.ModuleId == value)
            .ToListAsync();

        return lessons;
    }

    public async Task<IEnumerable<Lesson>> GetLessonsByModuleIds(List<int> ids)
    {
        
        var lessons = await _context.Lessons
            .Where(e => ids.Contains(e.ModuleId))
            .ToListAsync();

        return lessons;
    }
}
using System.Reflection.Metadata;
using Coursery.Domain.Entities;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coursery.Infrastucture.Repositories;

public class ModuleRepository : IModuleRepository
{
    public CourseryDbContext _context { get; set; }

    public ModuleRepository(CourseryDbContext context)
    {
        _context = context;
    }

    public async Task<Module> GetByIdAsync(int id)
    {
        return await _context.Modules.FindAsync(id);
    }
    
    public async Task<Module> GetByIdWithLessonsAsync(int id)
    {
        return await _context.Modules
            .Include(e => e.Lessons)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Module>> GetAllAsync()
    {
        return await _context.Modules.ToListAsync();
    }

    public async Task<Module> AddAsync(Module entity)
    {
        var created = await _context.Modules.AddAsync(entity);
        await _context.SaveChangesAsync();
        return created.Entity;
    }

    public async Task UpdateAsync(Module entity)
    {
        _context.Modules.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Modules.FindAsync(id);
        if (entity != null)
        {
            _context.Modules.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IList<Module>> GetModulesByCourseId(int courseId)
    {
        return await _context.Modules.Where(m => m.CourseId == courseId).ToListAsync();
    }
    
    
    
    public void Dispose()
    {
        _context.Dispose();
    }
}
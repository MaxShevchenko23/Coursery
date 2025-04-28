using Coursery.Domain.Entities;
using Coursery.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;

namespace Coursery.Infrastucture.Repositories;

public class ReviewRepository : IReviewRepository
{
    public CourseryDbContext _context { get; set; }
    
    public ReviewRepository(CourseryDbContext context)
    {
        _context = context;
    }

    public Task<Review> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Review>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Review> AddAsync(Review entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        return entity;
    }
    
    public async Task<IEnumerable<Review>> GetReviewsByCourseId(int courseId)
    {
        var reviews = await _context.Review
            .Where(r => r.CourseId == courseId)
            .ToListAsync();

        return reviews;
    }

    public Task UpdateAsync(Review entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}
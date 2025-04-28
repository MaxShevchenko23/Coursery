using Coursery.Domain.Entities;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coursery.Infrastucture.Repositories;

public class CourseRepository : ICourseRepository
{
   public CourseryDbContext _context { get; set; }
   
    public CourseRepository(CourseryDbContext context)
    {
        _context = context;
    }
   
    public async Task<Course> GetByIdAsync(int id)
    {
        var course = await _context.Courses
            .Include(e=>e.EnrolledStudents)
            .FirstAsync(e=>e.Id==id);
        
        if (course == null)
        {
            return null;
        }
        
        Console.WriteLine(course.Skills);
        
        var modules = await _context.Modules
            .Where(m => m.CourseId == id)
            .ToListAsync();

        Console.WriteLine("Modules retrieved");
        
        course.Modules= modules;
        
        foreach (var module in modules)
        {
            var lessons = await _context.Lessons
                .Where(l => l.ModuleId == module.Id)
                .ToListAsync();
            
            module.Lessons = lessons;
        }

        Console.WriteLine("Modules attached to course");
        
        return course;
    }
    
    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Courses.ToListAsync();
    }
    
    public async Task<Course> AddAsync(Course entity)
    {
        
        var created = await _context.Courses.AddAsync(entity);
        await _context.SaveChangesAsync();
        return created.Entity;
    }
    
    
    public async Task UpdateAsync(Course entity)
    {
        _context.Courses.Update(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Courses.FindAsync(id);
        if (entity != null)
        {
            _context.Courses.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IList<Course>> GetCoursesPaginated(int page, int pageSize)
    {
        var courses = 
            await _context.Courses
                .Skip((page-1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        
        return courses;
    }

    public async Task<IList<Course>> GetEnrolledCourses(int userId)
    {
        var enrolls =
            await _context
                .Enrolls
                .Include(e => e.Course)
                .Where(e => e.UserId == userId)
                .ToListAsync();

        var courses = enrolls.Select(e => e.Course).ToList();
        
        return courses;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<Course> Enroll((int userId, int courseId) value)
    {
        var enroll = new Enroll
        {
            UserId = value.userId,
            CourseId = value.courseId
        };
        
        var created=  await _context.Enrolls.AddAsync(enroll);
        await _context.SaveChangesAsync();

        return new Course();
    }
    
    public async Task<List<Course>> GetCreatedCourses(int userId)
    {
        var courses = await _context.Courses
            .Where(c => c.AuthorId  == userId)
            .ToListAsync();

        return courses;
    }
}
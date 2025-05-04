using Coursery.Domain.Entities;

namespace Coursery.Infrastucture.Interfaces;

public interface ICourseRepository  : IRepository<Course>
{
    Task<IList<Course>> GetCoursesPaginated(int page, int pageSize, string? category, decimal? minPrice, decimal? maxPrice, string? keyword);
}
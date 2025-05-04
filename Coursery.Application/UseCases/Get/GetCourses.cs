using Coursery.Application.Dtos.Get;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Repositories;
using Mapster;

namespace Coursery.Application.UseCases.Get;

public class GetCourses : BaseUseCase<(int page, int pageSize, string? keyword, string? category,
    decimal? minPrice, decimal? maxPrice), List<GetCourseShortDto>>
{
    public GetCourses(CourseryDbContext _context) : base(_context)
    {
    }

    public override async Task<List<GetCourseShortDto>> Execute
    ((int page, int pageSize, string? keyword, string? category,
        decimal? minPrice, decimal? maxPrice) value)
    {
        using (CourseRepository courseRepository = new(context))
        {
            var entities = await courseRepository.GetCoursesPaginated(
                value.page, 
                value.pageSize,
                value.category, 
                value.minPrice ?? 0, 
                value.maxPrice, 
                value.keyword);
            
            var dtos = entities.Adapt<List<GetCourseShortDto>>();
            return dtos;
        }
    }
}
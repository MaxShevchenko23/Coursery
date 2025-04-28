using Coursery.Application.Dtos.Get;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Repositories;
using Mapster;

namespace Coursery.Application.UseCases.Get;

public class GetCreatedCourses : BaseUseCase<int, List<GetCourseShortDto>>
{
    public GetCreatedCourses(CourseryDbContext _context) : base(_context)
    {
    }

    public override async Task<List<GetCourseShortDto>> Execute(int value)
    {
        using (CourseRepository repository = new(context))
        {
            var courses = await repository.GetCreatedCourses(value);

            return courses.Adapt<List<GetCourseShortDto>>();
        }
    }
}
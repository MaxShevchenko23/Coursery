using Coursery.Application.Dtos.Get;
using Coursery.Application.UseCases;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Repositories;
using Mapster;

namespace Coursery.Application.UseCases.Get;

public class GetEnrolledCourses : BaseUseCase<int, IList<GetCourseShortDto>>
{
    public GetEnrolledCourses(CourseryDbContext _context) : base(_context)
    {
    }

    public override async Task<IList<GetCourseShortDto>> Execute(int value)
    {
        using (CourseRepository repository = new(context))
        {
            var entities = await repository.GetEnrolledCourses(value);
            var dtos = entities.Adapt<IList<GetCourseShortDto>>();
            
            return dtos;
            
        }
    }
}
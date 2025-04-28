using System.Data;
using Coursery.Application.Dtos.Get;
using Coursery.Application.UseCases;
using Coursery.Domain.Entities;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Repositories;
using Mapster;

namespace Coursery.Application.UseCases.Add;

public class EnrollCourse : BaseUseCase<(int,int), GetCourseFullDto>
{
    public EnrollCourse(CourseryDbContext _context) : base(_context)
    {
    }

    public override async Task<GetCourseFullDto> Execute((int,int) value)
    {
        using (CourseRepository courseRepository = new(context))
        {
            var enroll = await courseRepository.Enroll(value);
            return enroll.Adapt<GetCourseFullDto>();
        }
    }
}
using Coursery.Application.Dtos.Get;
using Coursery.Domain.Entities;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Repositories;
using Mapster;

namespace Coursery.Application.UseCases.Add;

public class GetCourse : BaseUseCase<int, GetCourseFullDto>
{
    public GetCourse(CourseryDbContext _context) : base(_context)
    {
    }

    public override async Task<GetCourseFullDto> Execute(int id)
    {
        using (CourseRepository courseRepository = new(context))
        {
            var entity = await courseRepository.GetByIdAsync(id);

            var dto = new GetCourseFullDto()
            {

                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
                Name = entity.Name,
                Description = entity.Description,
                IntroImageUrl = entity.IntroImageUrl,
                Modules = entity.Modules.Adapt<List<GetModuleDto>>(),
                IntroVideoUrl = entity.IntroVideoUrl,
                Price = entity.Price,
                Categories = entity.Categories,
                Language = entity.Language,
                Status = entity.Status,
                Author = entity.Author.Adapt<GetUserDto>(),
                Skills = entity.Skills,
                EnrolledStudents = entity.EnrolledStudents.Count
            };
            
            Console.WriteLine("Course adapted to DTO");
            return dto;
        }
    }
}
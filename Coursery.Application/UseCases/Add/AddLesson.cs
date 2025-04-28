using Coursery.Application.Dtos.Get;
using Coursery.Domain.Entities;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Repositories;
using Mapster;

namespace Coursery.Application.UseCases.Add;

public class AddLesson : BaseUseCase<AddLessonDto, GetLessonDto>
{
    public AddLesson(CourseryDbContext _context) : base(_context)
    {
    }
    
    public override async Task<GetLessonDto> Execute(AddLessonDto value)
    {
        using (var lessonRepository = new LessonRepository(context))
        {
            var entity = value.Adapt<Lesson>();
            var lesson = await lessonRepository.AddAsync(entity);
            
            return lesson.Adapt<GetLessonDto>();
        }
    }
}
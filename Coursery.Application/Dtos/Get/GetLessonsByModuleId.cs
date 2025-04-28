using Coursery.Application.Dtos.Get;
using Coursery.Application.UseCases;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Repositories;
using Mapster;

namespace CourseryPL.Controllers;

public class GetLessonsByModuleId : BaseUseCase<int, List<GetLessonDto>>
{
    public GetLessonsByModuleId(CourseryDbContext _context) : base(_context)
    {
    }

    public override async Task<List<GetLessonDto>> Execute(int value)
    {
        using (LessonRepository lessonRepository = new(context))
        {
            var entities = await lessonRepository.GetLessonsByModuleId(value);
            var dtos = entities.Adapt<List<GetLessonDto>>();
            
            return dtos;
        }
    }
}
using Coursery.Application.Dtos.Get;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Repositories;
using Mapster;

namespace Coursery.Application.UseCases.Get;

public class GetLessonsForUser : BaseUseCase<(int page,int pageSize, int userId), List<GetLessonDto>>
{
    public GetLessonsForUser(CourseryDbContext _context) : base(_context)
    {
    }

    public override async Task<List<GetLessonDto>> Execute((int page,int pageSize, int userId) value)
    {
        using (LessonRepository repository = new(context))
        {
            var entities = await repository.GetLessonsPaginatedForUser
                (value.page, value.pageSize, value.userId);
            
            var dtos = entities.Adapt<List<GetLessonDto>>();
            return dtos;
        }
    }
}
using Coursery.Application.UseCases.Add;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Repositories;
using Mapster;

namespace Coursery.Application.UseCases.Get;

public class GetModulesByCourseId : BaseUseCase<int,IList<GetModuleDto>>
{
    public GetModulesByCourseId(CourseryDbContext _context) : base(_context)
    {
    }

    public override async Task<IList<GetModuleDto>> Execute(int value)
    {
        using (ModuleRepository moduleRepository = new(context))
        {
            var modules = await moduleRepository.GetModulesByCourseId(value);
            var dto = modules.Adapt<IList<GetModuleDto>>();
            
            return dto;
        }
    }
}
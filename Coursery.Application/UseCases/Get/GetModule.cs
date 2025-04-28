using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Repositories;
using Mapster;

namespace Coursery.Application.UseCases.Add;

public class GetModule : BaseUseCase<int, GetModuleDto>
{
    public GetModule(CourseryDbContext _context) : base(_context)
    {
    }

    public override async Task<GetModuleDto> Execute(int value)
    {
        using (ModuleRepository moduleRepository = new(context))
        {
            var entity = await moduleRepository.GetByIdAsync(value);
            var dto = entity.Adapt<GetModuleDto>();
            
             return dto;
        }
    }
}
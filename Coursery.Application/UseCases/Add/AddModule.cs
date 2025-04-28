using Coursery.Domain.Entities;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Repositories;
using Mapster;

namespace Coursery.Application.UseCases.Add;

public class AddModule : BaseUseCase<AddModuleDto, GetModuleDto>
{
    public AddModule(CourseryDbContext _context) : base(_context)
    {
    }

    public override async Task<GetModuleDto> Execute(AddModuleDto value)
    {
        var moduleEntity = value.Adapt<Module>();
        
        using (ModuleRepository moduleRepository = new ModuleRepository(context))
        {
            var created = await moduleRepository.AddAsync(moduleEntity);
            var moduleDto = created.Adapt<GetModuleDto>();
    
            return moduleDto;
        }
    }
}
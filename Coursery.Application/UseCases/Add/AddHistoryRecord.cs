using Coursery.Domain.Entities;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Repositories;
using Mapster;

namespace Coursery.Application.UseCases.Add;

public class AddHistoryRecord : BaseUseCase<AddHistoryRecordDto, History>
{
    public AddHistoryRecord(CourseryDbContext _context) : base(_context)
    {
    }

    public override async Task<History> Execute(AddHistoryRecordDto value)
    {
        var historyRecord = value.Adapt<History>();
        
        using (var historyRepository = new HistoryRepository(context))
        {
            var created = await historyRepository.AddAsync(historyRecord);
            return created;
        }
    }
}
using Coursery.Domain.Entities;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Repositories;

namespace Coursery.Application.UseCases.Add;

public class GetHistoryRecord : BaseUseCase<int, List<History>>
{
    public GetHistoryRecord(CourseryDbContext _context) : base(_context)
    {
    }

    public override async Task<List<History>> Execute(int userId)
    {
        using (var historyRepository = new HistoryRepository(context))
        {
            var historyRecord = await historyRepository.GetHistoriesByUserId(userId);
            return historyRecord;
        }
    }
}

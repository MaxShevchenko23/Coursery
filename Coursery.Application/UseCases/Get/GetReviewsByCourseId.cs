using Coursery.Application.Dtos.Get;
using Coursery.Infrastucture.Data;
using Mapster;

namespace Coursery.Application.UseCases.Get;

public class GetReviewsByCourseId: BaseUseCase<int, List<GetReviewDto>>
{
    public GetReviewsByCourseId(CourseryDbContext _context) : base(_context)
    {
    }

    public override async Task<List<GetReviewDto>> Execute(int value)
    {
        using (var reviewRepository = new Infrastucture.Repositories.ReviewRepository(context))
        {
            var reviews = await reviewRepository.GetReviewsByCourseId(value);
            return reviews.Adapt<List<GetReviewDto>>();
        }
    }
}
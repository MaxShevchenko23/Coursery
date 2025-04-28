using Coursery.Application.Dtos.Get;
using Coursery.Domain.Entities;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Repositories;
using Mapster;

namespace Coursery.Application.UseCases.Add;

public class AddReview : BaseUseCase<AddReviewDto, GetReviewDto>
{
    public AddReview(CourseryDbContext _context) : base(_context)
    {
    }

    public override async Task<GetReviewDto> Execute(AddReviewDto value)
    {
        var entity = value.Adapt<Review>();
        using (var reviewRepository = new ReviewRepository(context))
        {
            var created = await reviewRepository.AddAsync(entity);
            return created.Adapt<GetReviewDto>();
        }
    }
}
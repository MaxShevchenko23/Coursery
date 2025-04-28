using Coursery.Application.UseCases.Add;
using Coursery.Application.UseCases.Get;
using Microsoft.AspNetCore.Mvc;

namespace CourseryPL.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewController : ControllerBase
{
    private readonly AddReview _addReviewUseCase;
    private readonly GetReviewsByCourseId _getReviewsByCourseIdUseCase;

    public ReviewController(AddReview addReviewUseCase, GetReviewsByCourseId getReviewsByCourseIdUseCase)
    {
        _getReviewsByCourseIdUseCase = getReviewsByCourseIdUseCase;
        _addReviewUseCase = addReviewUseCase;
    }
    
    [HttpPost]
    public async Task<ActionResult> AddReview([FromBody] AddReviewDto request)
    {
        var response = await _addReviewUseCase.Execute(request);
        
        
        return Ok(response);
    }
    
    [HttpGet("{courseId}")]
    public async Task<ActionResult> GetReviewsByCourseId(int courseId)
    {
        var response = await _getReviewsByCourseIdUseCase.Execute(courseId);
        return Ok(response);
    }
}
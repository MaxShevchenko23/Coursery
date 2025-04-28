namespace Coursery.Application.Dtos.Get;

public class GetReviewDto
{
    public int Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public byte Rating { get; set; }

    public string Comment { get; set; }
    
    public int CourseId { get; set; }
    
    public int UserId { get; set; }
    public GetUserDto User { get; set; }
}
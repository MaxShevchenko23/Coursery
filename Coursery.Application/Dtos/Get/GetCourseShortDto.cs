using Coursery.Domain.Entities;

namespace Coursery.Application.Dtos.Get;

public class GetCourseShortDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public string? IntroImageUrl { get; set; }
    
    public decimal Price { get; set; }
    
    public string Categories { get; set; }
    
    public GetUserDto Author { get; set; }
}
using Coursery.Domain.Entities;

namespace Coursery.Application.UseCases.Add;

public class UpdateCourseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? IntroImageUrl { get; set; }
    public string? IntroVideoUrl { get; set; }
    public decimal Price { get; set; }

    public string Skills { get; set; }
    
    public string Categories { get; set; }
    
    public int Language { get; set; }
    
    public int? AuthorId { get; set; }   
    
    
    public IList<GetModuleDto> Modules { get; set; }
}
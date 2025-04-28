using Coursery.Domain.Entities;

namespace Coursery.Application.UseCases.Add;

public class AddCourseDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? IntroImageUrl { get; set; }
    public string? IntroVideoUrl { get; set; }
    public decimal Price { get; set; }

    public int AuthorId { get; set; }
    public List<string> Skills { get; set; }
    
    public List<string> Categories { get; set; }

    public List<AddModuleDto> Modules { get; set; }
    public Languages Language { get; set; }
    public Status Status { get; set; }
}
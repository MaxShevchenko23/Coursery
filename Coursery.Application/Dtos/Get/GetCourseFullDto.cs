using Coursery.Application.UseCases.Add;
using Coursery.Domain.Entities;

namespace Coursery.Application.Dtos.Get;

public class GetCourseFullDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? IntroImageUrl { get; set; }
    public string? IntroVideoUrl { get; set; }
    public decimal Price { get; set; }
    
    public string Skills { get; set; }

    public string Categories { get; set; }
    
    public Languages Language { get; set; }
    public Status Status { get; set; }
    
    public int AuthorId { get; set; }   
    public GetUserDto Author { get; set; }
    
    public IList<GetModuleDto> Modules { get; set; }
    public int EnrolledStudents { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Coursery.Application.UseCases.Add;

public class AddLessonDto
{
    [Required]
    public int ModuleId { get; set; }
    
    
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    
    public string? HtmlContent { get; set; }
    public string? VideoUrl { get; set; }
    public string? AdditionalResources { get; set; }
    
    public string? HomeworkQuestion { get; set; }
    
    public int OrderInModule { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace Coursery.Application.UseCases.Add;

public class AddModuleDto 
{
    [MaxLength(255)]
    public string Name { get; set; }
    
    [MaxLength(1000)]
    public string Description { get; set; }
    
    public int OrderInCourse { get; set; }

    public int CourseId { get; set; }
    
    public List<AddLessonDto> Lessons { get; set; }
}
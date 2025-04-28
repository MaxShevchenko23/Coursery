namespace Coursery.Domain.Entities;

public class Lesson : BaseEntity
{
    public int ModuleId { get; set; }
    
    public string Name { get; set; }
    public string Description { get; set; }
    
    public string? HtmlContent { get; set; }
    public string? VideoUrl { get; set; }
    public string? AdditionalResources { get; set; }
    
    public string HomeworkQuestion { get; set; }

    
    
    public int OrderInModule { get; set; }
    
    public Module Module { get; set; }
    
}
namespace Coursery.Application.UseCases.Add;

public class AddLessonShortDto
{
    public string Name { get; set; }
    
    public string? VideoUrl { get; set; }
    public string? AdditionalResources { get; set; }
    
    public string? HomeworkQuestion { get; set; }
    
    public int OrderInModule { get; set; }
}
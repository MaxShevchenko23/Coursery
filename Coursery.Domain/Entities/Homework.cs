namespace Coursery.Domain.Entities;

public class Homework : BaseEntity
{
    public int LessonId { get; set; }
    
    public string Answer { get; set; }
    
    public Lesson Lesson { get; set; }
    
    
}
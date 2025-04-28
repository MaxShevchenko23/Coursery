using System.Diagnostics.CodeAnalysis;

namespace Coursery.Domain.Entities;

public class Module : BaseEntity
{
    [NotNull]
    [MaxLength(255)]
    public string Name { get; set; }
    
    [NotNull]
    [MaxLength(1000)]
    public string Description { get; set; }
    
    public int OrderInCourse { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; }
    public IList<Lesson> Lessons { get; set; }
    
    
    public IList<Lesson> GetLessons()
    {
        return Lessons;
    }
    
    public Course GetCourse()
    {
        return Course;
    }
    
    public bool AddLesson(Lesson lesson)
    {
        if (lesson == null || string.IsNullOrEmpty(lesson.Name))
        {
            return false;
        }
    
        Lessons.Add(lesson);
        return true;
    }
    
    public bool RemoveLesson(Lesson lesson)
    {
        if (lesson == null || !Lessons.Contains(lesson))
        {
            return false;
        }
    
        Lessons.Remove(lesson);
        return true;
    }
    
    public bool IsAuthorOfCourse(int userId)
    {
        if (userId != Course.Author.Id)
        {
            return false;
        }
        
        return true;
    }
    
    private void UpdateModuleOrders(int firstModule, int secondModule, int newFirstModuleOrder, int newSecondModuleOrder)
    {
        
    }
}
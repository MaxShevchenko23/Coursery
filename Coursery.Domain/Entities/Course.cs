namespace Coursery.Domain.Entities;

public class Course : BaseEntity
{
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
    public User Author { get; set; }
    
    public IList<Review> Reviews { get; set; }
    public IList<Module> Modules { get; set; }
    public IList<Enroll> EnrolledStudents { get; set; }

    
    public IList<Module> GetCourseProgram()
    {
        return Modules;
    }
    
    public User GetAuthor()
    {
        return Author;
    }
    
    public decimal? GetPrice()
    {
        return Price == 0 ? (decimal?)null : Price;
    }
    
    public Status GetStatus()
    {
        return Status;
    }
    
    public bool IsPublic()
    {
        return Status == Status.Published;
    }
    
    public bool IsFree()
    {
        return Price == 0;
    }
    
    public int GetEnrolledStudentsCount()
    {
        // Assuming there is a property or method to get the count of enrolled students
        return EnrolledStudents.Count;
    }
    
    public bool AddModule(Module module)
    {
        if (Author == null || module == null || string.IsNullOrWhiteSpace(module.Name))
        {
            return false;
        }
    
        Modules.Add(module);
        return true;
    }
    
    public bool RemoveModule(Module module)
    {
        if (Author == null || module == null || !Modules.Contains(module))
        {
            return false;
        }
    
        Modules.Remove(module);
        return true;
    }
}

public enum Languages
{
    English,
    Ukrainian,
    Polish,
}

public enum Status
{
    Draft,
    Published,
    Deleted,
}
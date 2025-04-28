namespace Coursery.Domain.Entities;

public class Review : BaseEntity
{
    [Range(1, 5)]
    public byte Rating { get; set; }

    [MaxLength(255)]
    public string Comment { get; set; }
    
    public int CourseId { get; set; }
    public Course Course { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
}
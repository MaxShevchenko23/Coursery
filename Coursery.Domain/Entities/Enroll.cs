using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Coursery.Domain.Entities;

public class Enroll : BaseEntity
{
    public int UserId { get; set; }
    public int CourseId { get; set; }
    
    public User User { get; set; }
    public Course Course { get; set; }
}
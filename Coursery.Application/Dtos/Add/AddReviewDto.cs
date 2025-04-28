using System.ComponentModel.DataAnnotations;

namespace Coursery.Application.UseCases.Add;

public class AddReviewDto
{
    public int CourseId { get; set; }
    public int UserId { get; set; }
    
    [Range(1, 5)]
    public byte Rating { get; set; }

    [MaxLength(255)]
    public string Comment { get; set; }
}
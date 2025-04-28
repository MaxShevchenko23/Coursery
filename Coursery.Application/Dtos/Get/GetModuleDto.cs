using Coursery.Application.Dtos.Get;

namespace Coursery.Application.UseCases.Add;

public class GetModuleDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int OrderInCourse { get; set; }

    public int CourseId { get; set; }
    public IList<GetLessonDto> Lessons { get; set; }
}
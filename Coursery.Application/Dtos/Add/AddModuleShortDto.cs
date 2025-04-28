using Coursery.Domain.Entities;

namespace Coursery.Application.UseCases.Add;

public class AddModuleShortDto
{
    public string Title { get; set; }
    public List<AddLessonDto> Lessons  { get; set; }
}
using Coursery.Application.Dtos.Get;
using Coursery.Domain.Entities;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Repositories;
using Mapster;

namespace Coursery.Application.UseCases.Add;

public class AddCourse : BaseUseCase<AddCourseDto, GetCourseFullDto>
{
    public AddCourse(CourseryDbContext _context) : base(_context)
    {
    }

    public override async Task<GetCourseFullDto> Execute(AddCourseDto value)
    {
        using CourseRepository courseRepository = new(context);
        using ModuleRepository moduleRepository = new(context);
        using LessonRepository lessonRepository = new(context);
        
        var course = new Course()
        {
            Name = value.Name,
            Description = value.Description,
            IntroImageUrl = value.IntroImageUrl,
            IntroVideoUrl = value.IntroVideoUrl,
            Price = value.Price,
            Skills = string.Join(',', value.Skills),
            Categories = string.Join(',', value.Categories),
            Language = value.Language,
            Status = value.Status,
            AuthorId = value.AuthorId,
        };
        
        var created = await courseRepository.AddAsync(course);
        course = created;


        foreach (AddModuleDto moduleDto in value.Modules)
        {
            var module = new Module()
            {
                Name = moduleDto.Name,
                Description = moduleDto.Description,
                OrderInCourse = moduleDto.OrderInCourse,
                CourseId = course.Id
            };
                
            var createdModule = await moduleRepository.AddAsync(module);
            
            foreach (var lessonDto in moduleDto.Lessons)
            {
                var lesson = new Lesson()
                {
                    Name = lessonDto.Name,
                    Description = lessonDto.Description,
                    OrderInModule = lessonDto.OrderInModule,
                    ModuleId = createdModule.Id,
                    HomeworkQuestion = "lorem",
                    AdditionalResources = "lorem",
                    HtmlContent = "lorem",
                    VideoUrl = lessonDto.VideoUrl,
                };
                
                var createdLesson = await lessonRepository.AddAsync(lesson);

                createdModule.Lessons.Add(createdLesson);
            }
            
            course.Modules.Add(createdModule);
        }

        return course.Adapt<GetCourseFullDto> ();
    }
}
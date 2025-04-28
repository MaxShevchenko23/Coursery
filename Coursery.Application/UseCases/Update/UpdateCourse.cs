using Coursery.Application.Dtos.Get;
using Coursery.Domain.Entities;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Repositories;
using Mapster;

namespace Coursery.Application.UseCases.Add;

public class UpdateCourse : BaseUseCase<UpdateCourseDto, GetCourseShortDto>
{
    public UpdateCourse(CourseryDbContext _context) : base(_context)
    {
    }

    public override async Task<GetCourseShortDto> Execute(UpdateCourseDto value)
    {
        var courseRepository = new CourseRepository(context);
    
        // checks if course exists
        var oldCourse = await courseRepository.GetByIdAsync(value.Id);
    
        if (oldCourse == null)
        {
            throw new Exception("Course not found");
        }
    
        // checks and sets new values
        SetChanges(value, oldCourse);
    
        await courseRepository.UpdateAsync(oldCourse);
    
        // deletes modules that are not in the new list
        var moduleRepository = new ModuleRepository(context);
        var modules = await moduleRepository.GetModulesByCourseId(oldCourse.Id);
    
        if (modules.Count() != value.Modules.Count())
        {
            var deletedModulesIds = modules.Select(m => m.Id).Except(value.Modules.Select(m => m.Id)).ToList();
            foreach (var deletedModuleId in deletedModulesIds)
            {
                await moduleRepository.DeleteAsync(deletedModuleId);
            }
        }
    
        // updates modules
        foreach (var module in modules)
        {
            var updatedModule = value.Modules
                .FirstOrDefault(m => m.Id == module.Id);
    
            if (updatedModule == null)
            {
                // Удаляем модуль, если его нет в новом списке
                await moduleRepository.DeleteAsync(module.Id);
            }
            else
            {
                // Обновляем информацию в модуле
                module.Description = updatedModule.Description;
                module.Name = updatedModule.Name;
                module.OrderInCourse = updatedModule.OrderInCourse;
    
                await moduleRepository.UpdateAsync(module);
            }
        }
    
        var lessonRepository = new LessonRepository(context);
        var lessons = await lessonRepository.GetLessonsByModuleIds(modules.Select(m => m.Id).ToList());
    
        foreach (var lesson in lessons)
        {
            var updatedLesson = value.Modules
                .SelectMany(m => m.Lessons)
                .FirstOrDefault(l => l.Id == lesson.Id);
    
            if (updatedLesson == null)
            {
                // Удаляем урок, если его нет в новом списке
                await lessonRepository.DeleteAsync(lesson.Id);
            }
            else
            {
                // Обновляем информацию в уроке
                lesson.Description = updatedLesson.Description;
                lesson.Name = updatedLesson.Name;
                lesson.AdditionalResources = updatedLesson.AdditionalResources;
                lesson.VideoUrl = updatedLesson.VideoUrl;
                lesson.OrderInModule = updatedLesson.OrderInModule;
    
                await lessonRepository.UpdateAsync(lesson);
            }
        }
    
        var updatedCourse = await courseRepository.GetByIdAsync(oldCourse.Id);
        var dto = updatedCourse.Adapt<GetCourseShortDto>();
        return dto;
    }
    
    private static Course SetChanges(UpdateCourseDto value, Course oldCourse)
    {
        if (value.Name != null || value.Name != "") oldCourse.Name = value.Name;
        if (value.Description != null || value.Description != "") oldCourse.Description = value.Description;
        if (value.Price != null) oldCourse.Price = value.Price;
        if (value.Categories != null || value.Categories != "") oldCourse.Categories = value.Categories;
        
        if (value.Skills != null || value.Skills != "") oldCourse.Skills = value.Skills;
        if (value.Language != null) oldCourse.Language = (Languages)value.Language;
        if (value.IntroImageUrl != null || value.IntroImageUrl != "") oldCourse.IntroImageUrl = value.IntroImageUrl;
        if (value.IntroVideoUrl != null || value.IntroVideoUrl != "") oldCourse.IntroVideoUrl = value.IntroVideoUrl;

        return oldCourse;
    }
}
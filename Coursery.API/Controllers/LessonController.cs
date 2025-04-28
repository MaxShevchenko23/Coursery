using Coursery.Application.UseCases.Add;
using Coursery.Application.UseCases.Get;
using CourseryPL.Controllers.CourseryPL.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CourseryPL.Controllers;

[ApiController]
[Route("api/lessons")]
public class LessonController : ControllerBase 
{
    private readonly GetLessonsForUser _getLessonsForUser;
    private readonly GetLessonsByModuleId _getLessonsByModuleId;
    private readonly AddLesson _addLessonUseCase;

    public LessonController(GetLessonsForUser getLessonsForUser, AddLesson addLessonUseCase, GetLessonsByModuleId getLessonsByModuleId)
    {
        _addLessonUseCase = addLessonUseCase;
        _getLessonsByModuleId = getLessonsByModuleId;
        _getLessonsForUser = getLessonsForUser;
    }
    
    [HttpPost]
    public async Task<ActionResult> AddLesson(AddLessonDto request)
    {
        var response = await _addLessonUseCase.Execute(request);
        return Ok(response);
    }
    
    
    [HttpGet]
    public async Task<ActionResult> GetLessons(int page = 1, int pageSize = 10)
    {
        var userDto = JwtHelper.DecodeToken(HttpContext.Request.Headers["Authorization"]);
        
        if (userDto == null)
        {
            return Unauthorized();
        }
        
        var response = await _getLessonsForUser.Execute((page, pageSize, userDto.Id));
        return Ok(response);
    }
    
    [HttpGet("module/{moduleId}")]
    public async Task<ActionResult> GetLessonsByModuleId(int moduleId)
    {
        var userDto = JwtHelper.DecodeToken(HttpContext.Request.Headers["Authorization"]);
        
        if (userDto == null)
        {
            return Unauthorized();
        }

        var response = await _getLessonsByModuleId.Execute(moduleId);
        return Ok(response);
    }
    
}
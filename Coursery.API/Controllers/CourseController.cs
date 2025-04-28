using Coursery.Application.UseCases.Add;
using Coursery.Application.UseCases.Get;
using Coursery.Domain.Entities;
using CourseryPL.Controllers.CourseryPL.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseryPL.Controllers;

[Route("api/courses")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly AddCourse _addCourseUseCase;
    private readonly GetCourse _getCourseUseCase;
    private readonly GetCourses _getCoursesUseCase;
    private readonly GetEnrolledCourses _getEnrolledCoursesUseCase;
    private readonly EnrollCourse _enrollCourseUseCase;
    private readonly GetCreatedCourses _getCreatedCoursesUseCase;
    private readonly DeleteCourse _deleteCourseUseCase;
    private readonly UpdateCourse _updateCourseUseCase;

    public CourseController(AddCourse addCourseUseCase, GetCourse getCourseUseCase, 
        GetCourses getCoursesUseCase, GetEnrolledCourses getEnrolledCoursesUseCase, 
        EnrollCourse enrollCourseUseCase, GetCreatedCourses getCreatedCoursesUseCase,
        DeleteCourse deleteCourseUseCase, UpdateCourse updateCourseUseCase)
    {
        _addCourseUseCase = addCourseUseCase;
        _getCourseUseCase = getCourseUseCase;
        _getCoursesUseCase = getCoursesUseCase;
        _getEnrolledCoursesUseCase = getEnrolledCoursesUseCase;
        _enrollCourseUseCase = enrollCourseUseCase;
        _getCreatedCoursesUseCase = getCreatedCoursesUseCase;
        _deleteCourseUseCase = deleteCourseUseCase;
        _updateCourseUseCase = updateCourseUseCase;
    }

    [HttpPost]
    public async Task<ActionResult> AddCourse(AddCourseDto request)
    {
        var userId = JwtHelper.DecodeToken(HttpContext.Request.Headers["Authorization"]).Id;
        
        if (userId == null)
        {
            return Unauthorized();
        }

        request.AuthorId = userId;
        
        var response = await _addCourseUseCase.Execute(request);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetCourse(int id)
    {
        var response = await _getCourseUseCase.Execute(id);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult> GetCourses(
        string? keyword, int? category,
        int? minPrice, int? maxPrice,
        int page = 1, int pageSize = 10)
    {
        var response = await _getCoursesUseCase.Execute(
            (page, pageSize, keyword, category, minPrice, maxPrice));
        return Ok(response);
    }

    [HttpGet("enrolled")]
    public async Task<ActionResult> GetEnrolledCourses()
    {
        var userDto = JwtHelper.DecodeToken(HttpContext.Request.Headers["Authorization"]);
        
        if (userDto == null)
        {
            return Unauthorized();
        }
        
        var courses = await _getEnrolledCoursesUseCase.Execute(userDto.Id);
        
        return Ok(courses);
    }
    
    [HttpPost("enroll")]
    public async Task<ActionResult> EnrollCourse(int courseId)
    {
        var userDto = JwtHelper.DecodeToken(HttpContext.Request.Headers["Authorization"]);
        
        if (userDto == null)
        {
            return Unauthorized();
        }

        var response = await _enrollCourseUseCase.Execute((userDto.Id, courseId));
        return Ok(response);
    }
    
    [HttpGet("/api/allowedCategories")]
    public async Task<ActionResult> GetAllowedCategories()
    {
        var categories = Enum.GetValues(typeof(Category))
            .Cast<Category>()
            .Select(c => c.ToString())
            .ToList();

        return Ok(categories);
    }
    
    [HttpGet("created-courses")]
    public async Task<ActionResult> GetMyCourses()
    {
        var userDto = JwtHelper.DecodeToken(HttpContext.Request.Headers["Authorization"]);
        
        if (userDto == null)
        {
            return Unauthorized();
        }
        
        var courses = await _getCreatedCoursesUseCase.Execute(userDto.Id);
        
        return Ok(courses);
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> DeleteCourse(int id)
    {
        var success = await _deleteCourseUseCase.Execute(id);
        if (success)
        {
            return Ok(new { message = "Course deleted successfully." });
        }
        else
        {
            return NotFound(new { message = "Course not found." });
        }
        
    }
    
    
    
    [HttpPut]
    public async Task<ActionResult> UpdateCourse(UpdateCourseDto request)
    {
        var userId = JwtHelper.DecodeToken(HttpContext.Request.Headers["Authorization"]).Id;
        
        if (userId == null)
        {
            return Unauthorized();
        }

        var response = await _updateCourseUseCase.Execute(request);
        return Ok(response);
    }
}

public enum Category
{
    Programming = 1,
    Design = 2,
    Marketing = 3,
    Business = 4,
    Music = 5,
    Language = 6,
    Health = 7,
    Science = 8,
    History = 9,
    Literature = 10,
}
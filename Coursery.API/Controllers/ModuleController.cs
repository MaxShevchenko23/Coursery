using Coursery.Application.UseCases.Add;
using Coursery.Application.UseCases.Get;
using Microsoft.AspNetCore.Mvc;

namespace CourseryPL.Controllers;

[ApiController]
[Route("api/modules")]
public class ModuleController : ControllerBase
{
    private readonly AddModule _addModuleUseCase;
    private readonly GetModule _getModuleUseCase;
    private readonly UpdateModule _updateModuleUseCase;
    private readonly DeleteModule _deleteModuleUseCase;
    private readonly GetModulesByCourseId _getModulesByCourseId;

    public ModuleController(AddModule addModuleUseCase, GetModule getModuleUseCase, UpdateModule updateModuleUseCase,
        DeleteModule deleteModuleUseCase, GetModulesByCourseId getModulesByCourseId)
    {
        _addModuleUseCase = addModuleUseCase;
        _getModuleUseCase = getModuleUseCase;
        _updateModuleUseCase = updateModuleUseCase;
        _deleteModuleUseCase = deleteModuleUseCase;
        _getModulesByCourseId = getModulesByCourseId;
    }
    
    [HttpPost]
    public async Task<ActionResult> AddModule(AddModuleDto request)
    {
        var response = await _addModuleUseCase.Execute(request);
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetModule(int id)
    {
        var response = await _getModuleUseCase.Execute(id);
        
        return Ok(response);
    }
    
    [HttpGet("course/{courseId}")]
    public async Task<ActionResult> GetModulesByCourseId(int courseId)
    {
        var response = await _getModulesByCourseId.Execute(courseId);
        
        return Ok(response);
    } 
}
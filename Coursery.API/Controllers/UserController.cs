using Coursery.Application.UseCases.Add;
using Coursery.Application.UseCases.Get;
using CourseryPL.Controllers.CourseryPL.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CourseryPL.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly AddUser addUserUseCase;
    private readonly GetUser _getUserUseCase;
    private readonly GetUserByEmailAndPassword _getUserByEmailAndPassword;
    private readonly UpdateUser _updateUserUseCase;


    public UserController(
        AddUser _addUserUseCase, 
        GetUser _getUserUseCase, 
        GetUserByEmailAndPassword getUserByEmailAndPassword,
        UpdateUser _updateUserUseCase)
    {
        addUserUseCase = _addUserUseCase;
        this._getUserUseCase = _getUserUseCase;
        _getUserByEmailAndPassword = getUserByEmailAndPassword;
        this._updateUserUseCase = _updateUserUseCase;
    }
    
    
   [HttpPost]
   [AllowAnonymous]
   public async Task<ActionResult> AddUser(AddUserDto request)
   { 
       
       var response = await addUserUseCase.Execute(request);
       var token = JwtHelper.GenerateToken(response);
       
       return Ok(new { token });
   }

   [HttpPost("login")]
   [AllowAnonymous]
   public async Task<ActionResult> Login([FromBody] LoginDto request)
   {
       var user = await _getUserByEmailAndPassword.Execute(request.Email, request.Password);

       if (user != null)
       {
           var token = JwtHelper.GenerateToken(user);
           return Ok(new { token });
       }

       return NotFound();
   }
   
   [HttpPut("edit")]
   public async Task<ActionResult> EditUserInfo(UpdateUserDto form)
   {
       var userDto = JwtHelper.DecodeToken(HttpContext.Request.Headers["Authorization"]);
       
       if (userDto == null || userDto.Id != form.Id)
       {
           return Unauthorized();
       }

       var updated = await _updateUserUseCase.Execute(form);

       var token = JwtHelper.GenerateToken(updated);
       
       return Ok(new { token });
   }
   
    [HttpGet("{id}")]
    public async Task<ActionResult> GetUser(int id)
    {
        var user = await _getUserUseCase.Execute(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
    
   
    
}

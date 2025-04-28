using System.ComponentModel.DataAnnotations;
using Coursery.Domain.Entities;

namespace Coursery.Application.Dtos.Get;

public class GetUserDto
{
    public int Id { get; set; }
    
    public string Email { get; set; }
    
    public string FirstName { get; set; }

    public string LastName { get; set; }
    
    public string? Photo { get; set; }
    
    public string? Profession { get; set; }
    public Role Role { get; set; }
}
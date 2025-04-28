using System.ComponentModel.DataAnnotations;
using Coursery.Domain.Entities;

namespace Coursery.Application.UseCases.Add;

public class AddUserDto
{

    [Required]
    [MaxLength(255)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string LastName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [PasswordComplexity]
    public string Password { get; set; }
    
    [Required]
    [Range(0, 2)]
    public int UserRole { get; set; }
}
﻿namespace Coursery.Application.UseCases.Add;

public class UpdateUserDto
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Photo { get; set; }
    public string? Profession { get; set; }
    
}
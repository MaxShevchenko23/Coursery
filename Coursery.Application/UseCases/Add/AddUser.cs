using Coursery.Application.Dtos.Get;
using Coursery.Domain.Entities;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Interfaces;
using Coursery.Infrastucture.Repositories;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Coursery.Application.UseCases.Add;

public class AddUser 
{
    private readonly CourseryDbContext context;

    public AddUser(CourseryDbContext _context)
    {
        context = _context;
    }
    
    public async Task<GetUserDto> Execute(AddUserDto user)
    {
        var entity = user.Adapt<User>();
        
        using (UserRepository userRepository = new(context))
        {
            var created = await userRepository.AddAsync(entity);

            var dto = created.Adapt<GetUserDto>();
            return dto;
        }
        
    }
    
}
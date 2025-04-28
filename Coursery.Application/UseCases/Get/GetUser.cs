using Coursery.Application.Dtos.Get;
using Coursery.Domain.Entities;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Repositories;
using Mapster;

namespace Coursery.Application.UseCases.Add;

public class GetUser
{
    private readonly CourseryDbContext context;

    public GetUser(CourseryDbContext _context)
    {
        context = _context;
    }
    
    public async Task<GetUserDto> Execute(int id)
    {
        using (UserRepository userRepository = new(context))
        {
            var user = await userRepository.GetByIdAsync(id);
            return user.Adapt<GetUserDto>();
        }
    }
   
}
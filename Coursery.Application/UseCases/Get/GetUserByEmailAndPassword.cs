using Coursery.Application.Dtos.Get;
using Coursery.Domain.Entities;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Repositories;
using Mapster;

namespace Coursery.Application.UseCases.Get;

public class GetUserByEmailAndPassword
{
    private readonly CourseryDbContext context;

    public GetUserByEmailAndPassword(CourseryDbContext _context)
    {
        context = _context;
    }

    public async Task<GetUserDto> Execute(string email, string password)
    {
        using (UserRepository userRepository = new(context))
        {
            var user = await userRepository.GetByEmailAndPasswordAsync(email, password);
            return user.Adapt<GetUserDto>();
        }
    }

}
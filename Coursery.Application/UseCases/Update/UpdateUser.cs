using Coursery.Application.Dtos.Get;
using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Repositories;
using Mapster;

namespace Coursery.Application.UseCases.Add;

public class UpdateUser : BaseUseCase<UpdateUserDto, GetUserDto>
{
    public UpdateUser(CourseryDbContext _context) : base(_context)
    {
    }


    public override async Task<GetUserDto> Execute(UpdateUserDto dto)
    {
        using (UserRepository userRepository = new UserRepository(context))
        {
            var user = await userRepository.GetByIdAsync(dto.Id);
            
            if (user == null)
            {
                throw new Exception("User not found");
            }
            
            if (dto.Email != null) user.Email= dto.Email;  
            if (dto.FirstName != null) user.FirstName = dto.FirstName;
            if (dto.LastName != null) user.LastName = dto.LastName;
            if (dto.Photo != null) user.Photo = dto.Photo;
            if (dto.Profession != null) user.Password = dto.Profession;
            
           
            
            await userRepository.UpdateAsync(user);

            return user.Adapt<GetUserDto>();
        }
    }
}
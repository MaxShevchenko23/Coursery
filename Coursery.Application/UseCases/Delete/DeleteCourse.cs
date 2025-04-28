using Coursery.Infrastucture.Data;
using Coursery.Infrastucture.Repositories;

namespace Coursery.Application.UseCases.Add;

public class DeleteCourse : BaseUseCase<int, bool>
{
    public DeleteCourse(CourseryDbContext _context) : base(_context)
    {
    }

    public override async Task<bool> Execute(int value)
    {
        using (CourseRepository courseRepository = new(context))
        {
            var course = await courseRepository.GetByIdAsync(value);
            if (course == null)
            {
                return false;
            }

            await courseRepository.DeleteAsync(value);
            return true;
        }
    }
}
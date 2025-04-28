using Coursery.Infrastucture.Data;

namespace Coursery.Application.UseCases
{
    public abstract class BaseUseCase<TRequest, TResponse>
    {
        protected readonly CourseryDbContext context;

        protected BaseUseCase(CourseryDbContext _context)
        {
            context = _context;
        }

        public abstract Task<TResponse> Execute(TRequest value);

    }
    
}
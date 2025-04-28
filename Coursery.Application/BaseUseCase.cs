namespace Coursery.Application;

public abstract class BaseUseCase<ReturnT, ParameterT>
{
    public abstract ReturnT? Execute(ParameterT parameter);
}
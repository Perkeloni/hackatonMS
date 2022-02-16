namespace Hackaton.Domain.Exceptions.NotFoundException
{
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException(string message) : base(message)
        {

        }
    }
}
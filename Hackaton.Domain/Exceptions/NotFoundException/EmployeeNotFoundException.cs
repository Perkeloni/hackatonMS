namespace Hackaton.Domain.Exceptions.NotFoundException
{
    public class EmployeeNotFoundException : NotFoundException
    {
        public EmployeeNotFoundException(Guid employeeId) : base($"The employee {employeeId} was not found.")
        {

        }
    }
}
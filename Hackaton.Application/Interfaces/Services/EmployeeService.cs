using Hackaton.Application.Interfaces.Persistence;

namespace Hackaton.Application.Interfaces.Services
{
    internal class EmployeeService
    {
        private IRepositoryManager repository;

        public EmployeeService(IRepositoryManager repository)
        {
            this.repository = repository;
        }
    }
}
using Hackaton.Application.Common.Models;
using System.Collections.Generic;
using System.Dynamic;

namespace Employee.UnitTests
{
    public static class EmployeeServiceData
    {
        public static PagedList<ExpandoObject> GetAllEmployees_WithOneEmployee()
        {
            Hackaton.Domain.Entities.Employee employee = new()
            {
                Name = "Sr.Teste",
                Email = "teste@mail.com"
            };
            var employeeList = new List<ExpandoObject>();
            dynamic expando = new ExpandoObject();
            expando.Name = employee.Name;
            expando.Email = employee.Email;
            employeeList.Add(expando);
            return new PagedList<ExpandoObject>(employeeList, employeeList.Count, 1, 10);
        }
        public static PagedList<ExpandoObject> GetAllEmployees_WithTwoEmployee()
        {
            List<Hackaton.Domain.Entities.Employee> employees = new()
            {
                new Hackaton.Domain.Entities.Employee()
                {
                    Name = "Sr.Teste",
                    Email = "teste@mail.com"
                },
                new Hackaton.Domain.Entities.Employee()
                {
                   Name = "Sra.Teste",
                   Email = "teste2@mail.com"
               }
            };
            var employeeList = new List<ExpandoObject>();
            foreach (var employee in employees)
            {
                dynamic expando = new ExpandoObject();
                expando.Name = employee.Name;
                expando.Email = employee.Email;
                employeeList.Add(expando);
            }
            return new PagedList<ExpandoObject>(employeeList, employeeList.Count, 1, 10);
        }
    }
}
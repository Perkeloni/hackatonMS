using Hackaton.Application.Common.Models;

namespace Hackaton.Application.Models.Employee;

public class EmployeeParameters : QueryStringParameters
{
    public string ContainsNameFilter { get; init; } = string.Empty;
    public string EmailFilter { get; init; } = string.Empty;

    public EmployeeParameters()
    {
        OrderBy = "Name";
    }
}
namespace Hackaton.Application.DataTransferObject
{
    public record EmployeeDto
    {
        public Guid Id { get; init; }
        public int OldId { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Email { get; init; }
        public string? NIF { get; init; }
        public string? Address { get; init; }
    }
}
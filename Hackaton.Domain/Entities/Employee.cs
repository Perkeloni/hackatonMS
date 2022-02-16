namespace Hackaton.Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public int OldId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? NIF { get; set; }
        public Address? Address { get; set; } = new Address();
    }
}
using Hackaton.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hackaton.Persistence.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable(nameof(Employee));
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Id).HasColumnName("EmployeeId");
            builder.Property(e => e.Email).HasMaxLength(64);
            builder.Property(e => e.Name).HasMaxLength(64);
            //builder.Property(e => e.NIF).HasMaxLength(24);

            builder.OwnsOne(e => e.Address, adr =>
            {
                adr.Property(a => a.Line1).HasMaxLength(64);
                adr.Property(a => a.Line2).HasMaxLength(64);
                adr.Property(a => a.City).HasMaxLength(64);
                adr.Property(a => a.State).HasMaxLength(64);
                adr.Property(a => a.PostalCode).HasMaxLength(24);
            });

            //builder.HasData(new Employee
            //{
            //    Id = new Guid("0CD7BD00-48C8-4EDD-8953-A925647077A6"),
            //    Email = "eu@omeumail.eu",
            //    NIF = "12312312",
            //    Name = "ines",
            //});
            //builder.OwnsOne(e => e.Address).HasData(new
            //{
            //    EmployeeId = new Guid("0CD7BD00-48C8-4EDD-8953-A925647077A6"),
            //    City = "TestCity",
            //    Line1 = "TestLine"
            //});
        }
    }
}
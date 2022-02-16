using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackaton.Persistence.Migrations
{
    public partial class createDbTest4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeId", "Email", "NIF", "Name", "OldId" },
                values: new object[] { new Guid("0cd7bd00-48c8-4edd-8953-a925647077a6"), "eu@omeumail.eu", "12312312", "ines", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "EmployeeId",
                keyValue: new Guid("0cd7bd00-48c8-4edd-8953-a925647077a6"));
        }
    }
}

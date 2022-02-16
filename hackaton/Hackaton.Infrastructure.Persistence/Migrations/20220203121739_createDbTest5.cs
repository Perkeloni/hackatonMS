using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackaton.Persistence.Migrations
{
    public partial class createDbTest5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "EmployeeId",
                keyValue: new Guid("0cd7bd00-48c8-4edd-8953-a925647077a6"),
                columns: new[] { "Address_City", "Address_Line1" },
                values: new object[] { "TestCity", "TestLine" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

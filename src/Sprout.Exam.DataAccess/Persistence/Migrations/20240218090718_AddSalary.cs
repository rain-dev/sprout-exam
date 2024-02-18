using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Sprout.Exam.Domain.Models;

namespace Sprout.Exam.DataAccess.Persistence.Migrations
{
    public partial class AddSalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("EmployeeType");

            migrationBuilder.AddColumn<Employee>("Salary", "Employee", "decimal(18,2)", schema: "dbo", nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeType", x => x.Id);
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "EmployeeType");
        }
    }
}

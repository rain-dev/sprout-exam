using Microsoft.EntityFrameworkCore.Migrations;

namespace Sprout.Exam.DataAccess.Persistence.Migrations
{
    public partial class AddEmployeeTypeSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "EmployeeType",
                newName: "TypeName");

            migrationBuilder.AlterColumn<decimal>(
                name: "Tax",
                table: "EmployeeType",
                type: "decimal(18,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "EmployeeType",
                columns: new[] { "Id", "Tax", "TypeName" },
                values: new object[] { 1, 12m, "Regular" });

            migrationBuilder.InsertData(
                table: "EmployeeType",
                columns: new[] { "Id", "Tax", "TypeName" },
                values: new object[] { 2, null, "Contractual" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EmployeeType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "TypeName",
                table: "EmployeeType",
                newName: "Name");

            migrationBuilder.AlterColumn<decimal>(
                name: "Tax",
                table: "EmployeeType",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,1)",
                oldNullable: true);
        }
    }
}

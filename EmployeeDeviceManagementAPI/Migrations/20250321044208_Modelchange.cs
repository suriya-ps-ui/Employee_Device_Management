using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeDeviceManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class Modelchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isAvilable",
                schema: "AssetManagement",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                schema: "AssetManagement",
                table: "Employees",
                keyColumn: "empId",
                keyValue: "E1",
                column: "isAvilable",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAvilable",
                schema: "AssetManagement",
                table: "Employees");
        }
    }
}

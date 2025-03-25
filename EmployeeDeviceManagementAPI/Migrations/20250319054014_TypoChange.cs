using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeDeviceManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class TypoChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "departmemt",
                schema: "AssetManagement",
                table: "Employees",
                newName: "department");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "department",
                schema: "AssetManagement",
                table: "Employees",
                newName: "departmemt");
        }
    }
}

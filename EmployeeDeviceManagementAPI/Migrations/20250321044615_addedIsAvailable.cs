using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeDeviceManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedIsAvailable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "AssetManagement",
                table: "Employees",
                keyColumn: "empId",
                keyValue: "E1",
                column: "isAvilable",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "AssetManagement",
                table: "Employees",
                keyColumn: "empId",
                keyValue: "E1",
                column: "isAvilable",
                value: false);
        }
    }
}

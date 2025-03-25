using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeDeviceManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class modifyModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "isAvilable",
                schema: "AssetManagement",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                schema: "AssetManagement",
                table: "Employees",
                keyColumn: "empId",
                keyValue: "E1",
                column: "isAvilable",
                value: "true");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isAvilable",
                schema: "AssetManagement",
                table: "Employees",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                schema: "AssetManagement",
                table: "Employees",
                keyColumn: "empId",
                keyValue: "E1",
                column: "isAvilable",
                value: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeDeviceManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                schema: "AssetManagement",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    empId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Employees_empId",
                        column: x => x.empId,
                        principalSchema: "AssetManagement",
                        principalTable: "Employees",
                        principalColumn: "empId");
                });

            migrationBuilder.InsertData(
                schema: "AssetManagement",
                table: "Employees",
                columns: new[] { "empId", "department", "empName" },
                values: new object[] { "E1", "DotNet", "Suriya" });

            migrationBuilder.InsertData(
                schema: "AssetManagement",
                table: "Users",
                columns: new[] { "id", "empId", "password", "role", "userName" },
                values: new object[,]
                {
                    { 1, null, "admin", "Admin", "admin" },
                    { 2, "E1", "e1", "Employee", "e1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_empId",
                schema: "AssetManagement",
                table: "Users",
                column: "empId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users",
                schema: "AssetManagement");

            migrationBuilder.DeleteData(
                schema: "AssetManagement",
                table: "Employees",
                keyColumn: "empId",
                keyValue: "E1");
        }
    }
}

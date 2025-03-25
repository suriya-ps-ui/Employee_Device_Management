using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeDeviceManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AssetManagement");

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "AssetManagement",
                columns: table => new
                {
                    empId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    empName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    departmemt = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.empId);
                });

            migrationBuilder.CreateTable(
                name: "Keyboards",
                schema: "AssetManagement",
                columns: table => new
                {
                    keyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    empId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    keyS_No = table.Column<int>(type: "int", nullable: false),
                    keyBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keyboards", x => x.keyId);
                    table.ForeignKey(
                        name: "FK_Keyboards_Employees_empId",
                        column: x => x.empId,
                        principalSchema: "AssetManagement",
                        principalTable: "Employees",
                        principalColumn: "empId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Laptops",
                schema: "AssetManagement",
                columns: table => new
                {
                    lapHostName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    empId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    lapModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    processor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    storage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ram = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    assignedOn = table.Column<DateOnly>(type: "date", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laptops", x => new { x.empId, x.lapHostName });
                    table.ForeignKey(
                        name: "FK_Laptops_Employees_empId",
                        column: x => x.empId,
                        principalSchema: "AssetManagement",
                        principalTable: "Employees",
                        principalColumn: "empId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mouses",
                schema: "AssetManagement",
                columns: table => new
                {
                    mouseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    empId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    mouseS_No = table.Column<int>(type: "int", nullable: false),
                    mouseBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mouses", x => x.mouseId);
                    table.ForeignKey(
                        name: "FK_Mouses_Employees_empId",
                        column: x => x.empId,
                        principalSchema: "AssetManagement",
                        principalTable: "Employees",
                        principalColumn: "empId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Keyboards_empId",
                schema: "AssetManagement",
                table: "Keyboards",
                column: "empId");

            migrationBuilder.CreateIndex(
                name: "IX_Mouses_empId",
                schema: "AssetManagement",
                table: "Mouses",
                column: "empId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Keyboards",
                schema: "AssetManagement");

            migrationBuilder.DropTable(
                name: "Laptops",
                schema: "AssetManagement");

            migrationBuilder.DropTable(
                name: "Mouses",
                schema: "AssetManagement");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "AssetManagement");
        }
    }
}

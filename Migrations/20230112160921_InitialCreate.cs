using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercaDariusExamenNew.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manager = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentsID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentsID",
                        column: x => x.DepartmentsID,
                        principalTable: "Departments",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manager = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentsID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Managers_Departments_DepartmentsID",
                        column: x => x.DepartmentsID,
                        principalTable: "Departments",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OutTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AbsenceReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Attendance_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_EmployeeID",
                table: "Attendance",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentsID",
                table: "Employees",
                column: "DepartmentsID");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_DepartmentsID",
                table: "Managers",
                column: "DepartmentsID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}

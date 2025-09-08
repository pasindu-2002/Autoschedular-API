using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace autoschedular.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignModuleAndLecturerTimetableWithNoAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LecturerTimetables_Lecturers_LecturerId",
                table: "LecturerTimetables");

            migrationBuilder.CreateTable(
                name: "AssignModules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BatchId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LecturerId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModuleId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SessionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignModules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignModules_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "BatchCode");
                    table.ForeignKey(
                        name: "FK_AssignModules_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "EmpNo");
                    table.ForeignKey(
                        name: "FK_AssignModules_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "ModuleCode");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignModules_BatchId",
                table: "AssignModules",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignModules_LecturerId",
                table: "AssignModules",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignModules_ModuleId",
                table: "AssignModules",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_LecturerTimetables_Lecturers_LecturerId",
                table: "LecturerTimetables",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "EmpNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LecturerTimetables_Lecturers_LecturerId",
                table: "LecturerTimetables");

            migrationBuilder.DropTable(
                name: "AssignModules");

            migrationBuilder.AddForeignKey(
                name: "FK_LecturerTimetables_Lecturers_LecturerId",
                table: "LecturerTimetables",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "EmpNo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

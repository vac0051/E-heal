using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHealthPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddHospitalizationAndSnils : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Iin",
                table: "Users",
                newName: "Snils");

            migrationBuilder.CreateTable(
                name: "Hospitalizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClinicId = table.Column<int>(type: "INTEGER", nullable: false),
                    RequestedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AdmissionDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Reason = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitalizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hospitalizations_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hospitalizations_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hospitalizations_ClinicId",
                table: "Hospitalizations",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitalizations_PatientId",
                table: "Hospitalizations",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hospitalizations");

            migrationBuilder.RenameColumn(
                name: "Snils",
                table: "Users",
                newName: "Iin");
        }
    }
}

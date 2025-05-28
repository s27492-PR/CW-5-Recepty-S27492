using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recepty.Migrations
{
    /// <inheritdoc />
    public partial class CreatePrescription_Medicamenttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicamentIdmedicament",
                table: "Prescription",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrescriptionIdPrescription",
                table: "Prescription",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_MedicamentIdmedicament",
                table: "Prescription",
                column: "MedicamentIdmedicament");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_PrescriptionIdPrescription",
                table: "Prescription",
                column: "PrescriptionIdPrescription");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Medicament_MedicamentIdmedicament",
                table: "Prescription",
                column: "MedicamentIdmedicament",
                principalTable: "Medicament",
                principalColumn: "Idmedicament");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Prescription_PrescriptionIdPrescription",
                table: "Prescription",
                column: "PrescriptionIdPrescription",
                principalTable: "Prescription",
                principalColumn: "IdPrescription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Medicament_MedicamentIdmedicament",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Prescription_PrescriptionIdPrescription",
                table: "Prescription");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_MedicamentIdmedicament",
                table: "Prescription");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_PrescriptionIdPrescription",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "MedicamentIdmedicament",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "PrescriptionIdPrescription",
                table: "Prescription");
        }
    }
}

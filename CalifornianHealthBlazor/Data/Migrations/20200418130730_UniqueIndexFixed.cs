using Microsoft.EntityFrameworkCore.Migrations;

namespace CalifornianHealthBlazor.Data.Migrations
{
    public partial class UniqueIndexFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointment_ConsultantFk_PatientFk_TimeSlotFk_SelectedDate",
                table: "Appointment");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_ConsultantFk_TimeSlotFk_SelectedDate",
                table: "Appointment",
                columns: new[] { "ConsultantFk", "TimeSlotFk", "SelectedDate" },
                unique: true,
                filter: "[ConsultantFk] IS NOT NULL AND [TimeSlotFk] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointment_ConsultantFk_TimeSlotFk_SelectedDate",
                table: "Appointment");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_ConsultantFk_PatientFk_TimeSlotFk_SelectedDate",
                table: "Appointment",
                columns: new[] { "ConsultantFk", "PatientFk", "TimeSlotFk", "SelectedDate" },
                unique: true,
                filter: "[ConsultantFk] IS NOT NULL AND [PatientFk] IS NOT NULL AND [TimeSlotFk] IS NOT NULL");
        }
    }
}

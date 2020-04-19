using Microsoft.EntityFrameworkCore.Migrations;

namespace CalifornianHealthBlazor.Data.Migrations
{
    public partial class ConsultantSpecificTimeSlots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConsultantFk",
                table: "TimeSlot",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "TimeSlot",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlot_ConsultantFk",
                table: "TimeSlot",
                column: "ConsultantFk");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlot_Consultant_ConsultantFk",
                table: "TimeSlot",
                column: "ConsultantFk",
                principalTable: "Consultant",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlot_Consultant_ConsultantFk",
                table: "TimeSlot");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlot_ConsultantFk",
                table: "TimeSlot");

            migrationBuilder.DropColumn(
                name: "ConsultantFk",
                table: "TimeSlot");

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "TimeSlot");
        }
    }
}

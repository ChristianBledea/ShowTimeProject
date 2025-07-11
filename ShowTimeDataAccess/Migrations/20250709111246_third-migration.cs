using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowTime.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class thirdmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FestivalId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FestivalId",
                table: "Tickets",
                column: "FestivalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Festivals_FestivalId",
                table: "Tickets",
                column: "FestivalId",
                principalTable: "Festivals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Festivals_FestivalId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_FestivalId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "FestivalId",
                table: "Tickets");
        }
    }
}

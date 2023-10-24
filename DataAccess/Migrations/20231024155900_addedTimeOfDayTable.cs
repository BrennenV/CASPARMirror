using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addedTimeOfDayTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WishlistPartOfDay",
                table: "WishlistDetailModalities");

            migrationBuilder.AddColumn<int>(
                name: "TimeOfDayId",
                table: "WishlistDetailModalities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TimeOfDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartOfDay = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeOfDays", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WishlistDetailModalities_TimeOfDayId",
                table: "WishlistDetailModalities",
                column: "TimeOfDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistDetailModalities_TimeOfDays_TimeOfDayId",
                table: "WishlistDetailModalities",
                column: "TimeOfDayId",
                principalTable: "TimeOfDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishlistDetailModalities_TimeOfDays_TimeOfDayId",
                table: "WishlistDetailModalities");

            migrationBuilder.DropTable(
                name: "TimeOfDays");

            migrationBuilder.DropIndex(
                name: "IX_WishlistDetailModalities_TimeOfDayId",
                table: "WishlistDetailModalities");

            migrationBuilder.DropColumn(
                name: "TimeOfDayId",
                table: "WishlistDetailModalities");

            migrationBuilder.AddColumn<string>(
                name: "WishlistPartOfDay",
                table: "WishlistDetailModalities",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

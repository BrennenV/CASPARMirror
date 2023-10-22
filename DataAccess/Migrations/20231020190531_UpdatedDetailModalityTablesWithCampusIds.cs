using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDetailModalityTablesWithCampusIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishlistDetailModalities_DaysOfWeeks_DaysOfWeekId",
                table: "WishlistDetailModalities");

            migrationBuilder.RenameColumn(
                name: "DaysOfWeekId",
                table: "WishlistDetailModalities",
                newName: "CampusId");

            migrationBuilder.RenameIndex(
                name: "IX_WishlistDetailModalities_DaysOfWeekId",
                table: "WishlistDetailModalities",
                newName: "IX_WishlistDetailModalities_CampusId");

            migrationBuilder.AddColumn<int>(
                name: "CampusId",
                table: "PreferenceListDetailModalities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PreferenceListDetailModalities_CampusId",
                table: "PreferenceListDetailModalities",
                column: "CampusId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceListDetailModalities_Campuses_CampusId",
                table: "PreferenceListDetailModalities",
                column: "CampusId",
                principalTable: "Campuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistDetailModalities_Campuses_CampusId",
                table: "WishlistDetailModalities",
                column: "CampusId",
                principalTable: "Campuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceListDetailModalities_Campuses_CampusId",
                table: "PreferenceListDetailModalities");

            migrationBuilder.DropForeignKey(
                name: "FK_WishlistDetailModalities_Campuses_CampusId",
                table: "WishlistDetailModalities");

            migrationBuilder.DropIndex(
                name: "IX_PreferenceListDetailModalities_CampusId",
                table: "PreferenceListDetailModalities");

            migrationBuilder.DropColumn(
                name: "CampusId",
                table: "PreferenceListDetailModalities");

            migrationBuilder.RenameColumn(
                name: "CampusId",
                table: "WishlistDetailModalities",
                newName: "DaysOfWeekId");

            migrationBuilder.RenameIndex(
                name: "IX_WishlistDetailModalities_CampusId",
                table: "WishlistDetailModalities",
                newName: "IX_WishlistDetailModalities_DaysOfWeekId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistDetailModalities_DaysOfWeeks_DaysOfWeekId",
                table: "WishlistDetailModalities",
                column: "DaysOfWeekId",
                principalTable: "DaysOfWeeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

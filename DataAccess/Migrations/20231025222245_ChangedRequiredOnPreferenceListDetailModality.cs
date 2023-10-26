using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangedRequiredOnPreferenceListDetailModality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceListDetailModalities_Campuses_CampusId",
                table: "PreferenceListDetailModalities");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceListDetailModalities_DaysOfWeeks_DaysOfWeekId",
                table: "PreferenceListDetailModalities");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceListDetailModalities_TimeBlocks_TimeBlockId",
                table: "PreferenceListDetailModalities");

            migrationBuilder.AlterColumn<int>(
                name: "TimeBlockId",
                table: "PreferenceListDetailModalities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DaysOfWeekId",
                table: "PreferenceListDetailModalities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CampusId",
                table: "PreferenceListDetailModalities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceListDetailModalities_Campuses_CampusId",
                table: "PreferenceListDetailModalities",
                column: "CampusId",
                principalTable: "Campuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceListDetailModalities_DaysOfWeeks_DaysOfWeekId",
                table: "PreferenceListDetailModalities",
                column: "DaysOfWeekId",
                principalTable: "DaysOfWeeks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceListDetailModalities_TimeBlocks_TimeBlockId",
                table: "PreferenceListDetailModalities",
                column: "TimeBlockId",
                principalTable: "TimeBlocks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceListDetailModalities_Campuses_CampusId",
                table: "PreferenceListDetailModalities");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceListDetailModalities_DaysOfWeeks_DaysOfWeekId",
                table: "PreferenceListDetailModalities");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceListDetailModalities_TimeBlocks_TimeBlockId",
                table: "PreferenceListDetailModalities");

            migrationBuilder.AlterColumn<int>(
                name: "TimeBlockId",
                table: "PreferenceListDetailModalities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DaysOfWeekId",
                table: "PreferenceListDetailModalities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CampusId",
                table: "PreferenceListDetailModalities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceListDetailModalities_Campuses_CampusId",
                table: "PreferenceListDetailModalities",
                column: "CampusId",
                principalTable: "Campuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceListDetailModalities_DaysOfWeeks_DaysOfWeekId",
                table: "PreferenceListDetailModalities",
                column: "DaysOfWeekId",
                principalTable: "DaysOfWeeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceListDetailModalities_TimeBlocks_TimeBlockId",
                table: "PreferenceListDetailModalities",
                column: "TimeBlockId",
                principalTable: "TimeBlocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

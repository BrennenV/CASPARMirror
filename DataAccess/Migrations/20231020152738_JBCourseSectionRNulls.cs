using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class JBCourseSectionRNulls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_Classrooms_ClassroomId",
                table: "CourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_DaysOfWeeks_DaysOfWeekId",
                table: "CourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_Instructors_InstructorId",
                table: "CourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_Modalities_ModalityId",
                table: "CourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_PartOfTerms_PartOfTermId",
                table: "CourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_PayModels_PayModelId",
                table: "CourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_PayOrganizations_PayOrganizationId",
                table: "CourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_SectionStatuses_SectionStatusId",
                table: "CourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_TimeBlocks_TimeBlockId",
                table: "CourseSections");

            migrationBuilder.AlterColumn<int>(
                name: "TimeBlockId",
                table: "CourseSections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SectionStatusId",
                table: "CourseSections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PayOrganizationId",
                table: "CourseSections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PayModelId",
                table: "CourseSections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PartOfTermId",
                table: "CourseSections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ModalityId",
                table: "CourseSections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "InstructorId",
                table: "CourseSections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DaysOfWeekId",
                table: "CourseSections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ClassroomId",
                table: "CourseSections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_Classrooms_ClassroomId",
                table: "CourseSections",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_DaysOfWeeks_DaysOfWeekId",
                table: "CourseSections",
                column: "DaysOfWeekId",
                principalTable: "DaysOfWeeks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_Instructors_InstructorId",
                table: "CourseSections",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_Modalities_ModalityId",
                table: "CourseSections",
                column: "ModalityId",
                principalTable: "Modalities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_PartOfTerms_PartOfTermId",
                table: "CourseSections",
                column: "PartOfTermId",
                principalTable: "PartOfTerms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_PayModels_PayModelId",
                table: "CourseSections",
                column: "PayModelId",
                principalTable: "PayModels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_PayOrganizations_PayOrganizationId",
                table: "CourseSections",
                column: "PayOrganizationId",
                principalTable: "PayOrganizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_SectionStatuses_SectionStatusId",
                table: "CourseSections",
                column: "SectionStatusId",
                principalTable: "SectionStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_TimeBlocks_TimeBlockId",
                table: "CourseSections",
                column: "TimeBlockId",
                principalTable: "TimeBlocks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_Classrooms_ClassroomId",
                table: "CourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_DaysOfWeeks_DaysOfWeekId",
                table: "CourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_Instructors_InstructorId",
                table: "CourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_Modalities_ModalityId",
                table: "CourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_PartOfTerms_PartOfTermId",
                table: "CourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_PayModels_PayModelId",
                table: "CourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_PayOrganizations_PayOrganizationId",
                table: "CourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_SectionStatuses_SectionStatusId",
                table: "CourseSections");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_TimeBlocks_TimeBlockId",
                table: "CourseSections");

            migrationBuilder.AlterColumn<int>(
                name: "TimeBlockId",
                table: "CourseSections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SectionStatusId",
                table: "CourseSections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PayOrganizationId",
                table: "CourseSections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PayModelId",
                table: "CourseSections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PartOfTermId",
                table: "CourseSections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModalityId",
                table: "CourseSections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InstructorId",
                table: "CourseSections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DaysOfWeekId",
                table: "CourseSections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClassroomId",
                table: "CourseSections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_Classrooms_ClassroomId",
                table: "CourseSections",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_DaysOfWeeks_DaysOfWeekId",
                table: "CourseSections",
                column: "DaysOfWeekId",
                principalTable: "DaysOfWeeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_Instructors_InstructorId",
                table: "CourseSections",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_Modalities_ModalityId",
                table: "CourseSections",
                column: "ModalityId",
                principalTable: "Modalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_PartOfTerms_PartOfTermId",
                table: "CourseSections",
                column: "PartOfTermId",
                principalTable: "PartOfTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_PayModels_PayModelId",
                table: "CourseSections",
                column: "PayModelId",
                principalTable: "PayModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_PayOrganizations_PayOrganizationId",
                table: "CourseSections",
                column: "PayOrganizationId",
                principalTable: "PayOrganizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_SectionStatuses_SectionStatusId",
                table: "CourseSections",
                column: "SectionStatusId",
                principalTable: "SectionStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_TimeBlocks_TimeBlockId",
                table: "CourseSections",
                column: "TimeBlockId",
                principalTable: "TimeBlocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

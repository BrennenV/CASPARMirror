using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MadeGiantChangesToWishlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Students_StudentId",
                table: "Wishlists");

            migrationBuilder.DropTable(
                name: "PreferenceListDetailModalities");

            migrationBuilder.DropTable(
                name: "WishlistDetailModalities");

            migrationBuilder.DropTable(
                name: "PreferenceListDetails");

            migrationBuilder.DropTable(
                name: "TimeOfDays");

            migrationBuilder.DropTable(
                name: "WishlistDetails");

            migrationBuilder.DropTable(
                name: "PreferenceLists");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_StudentId",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Wishlists");

            migrationBuilder.RenameColumn(
                name: "DaysOfWeekTitle",
                table: "DaysOfWeeks",
                newName: "DaysOfWeekValue");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Wishlists",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PartOfDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartOfDayValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartOfDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WishlistCampuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    WishlistId = table.Column<int>(type: "int", nullable: false),
                    CampusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistCampuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistCampuses_Campuses_CampusId",
                        column: x => x.CampusId,
                        principalTable: "Campuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistCampuses_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreferenceRank = table.Column<int>(type: "int", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    WishlistId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistCourses_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistDaysOfWeeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    WishlistId = table.Column<int>(type: "int", nullable: false),
                    DaysOfWeekId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistDaysOfWeeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistDaysOfWeeks_DaysOfWeeks_DaysOfWeekId",
                        column: x => x.DaysOfWeekId,
                        principalTable: "DaysOfWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistDaysOfWeeks_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistModalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    WishlistId = table.Column<int>(type: "int", nullable: false),
                    ModalityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistModalities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistModalities_Modalities_ModalityId",
                        column: x => x.ModalityId,
                        principalTable: "Modalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistModalities_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistTimeBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    WishlistId = table.Column<int>(type: "int", nullable: false),
                    TimeBlockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistTimeBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistTimeBlocks_TimeBlocks_TimeBlockId",
                        column: x => x.TimeBlockId,
                        principalTable: "TimeBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistTimeBlocks_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistPartOfDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    WishlistId = table.Column<int>(type: "int", nullable: false),
                    PartOfDayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistPartOfDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistPartOfDays_PartOfDays_PartOfDayId",
                        column: x => x.PartOfDayId,
                        principalTable: "PartOfDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistPartOfDays_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistCampuses_CampusId",
                table: "WishlistCampuses",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistCampuses_WishlistId",
                table: "WishlistCampuses",
                column: "WishlistId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistCourses_CourseId",
                table: "WishlistCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistCourses_WishlistId",
                table: "WishlistCourses",
                column: "WishlistId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistDaysOfWeeks_DaysOfWeekId",
                table: "WishlistDaysOfWeeks",
                column: "DaysOfWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistDaysOfWeeks_WishlistId",
                table: "WishlistDaysOfWeeks",
                column: "WishlistId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistModalities_ModalityId",
                table: "WishlistModalities",
                column: "ModalityId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistModalities_WishlistId",
                table: "WishlistModalities",
                column: "WishlistId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistPartOfDays_PartOfDayId",
                table: "WishlistPartOfDays",
                column: "PartOfDayId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistPartOfDays_WishlistId",
                table: "WishlistPartOfDays",
                column: "WishlistId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistTimeBlocks_TimeBlockId",
                table: "WishlistTimeBlocks",
                column: "TimeBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistTimeBlocks_WishlistId",
                table: "WishlistTimeBlocks",
                column: "WishlistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_AspNetUsers_UserId",
                table: "Wishlists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_AspNetUsers_UserId",
                table: "Wishlists");

            migrationBuilder.DropTable(
                name: "WishlistCampuses");

            migrationBuilder.DropTable(
                name: "WishlistCourses");

            migrationBuilder.DropTable(
                name: "WishlistDaysOfWeeks");

            migrationBuilder.DropTable(
                name: "WishlistModalities");

            migrationBuilder.DropTable(
                name: "WishlistPartOfDays");

            migrationBuilder.DropTable(
                name: "WishlistTimeBlocks");

            migrationBuilder.DropTable(
                name: "PartOfDays");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Wishlists");

            migrationBuilder.RenameColumn(
                name: "DaysOfWeekValue",
                table: "DaysOfWeeks",
                newName: "DaysOfWeekTitle");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Wishlists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PreferenceLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    SemesterInstanceId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferenceLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreferenceLists_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreferenceLists_SemesterInstances_SemesterInstanceId",
                        column: x => x.SemesterInstanceId,
                        principalTable: "SemesterInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeOfDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    PartOfDay = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeOfDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WishlistDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    WishlistId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistDetails_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistDetails_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreferenceListDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    PreferenceListId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    PreferenceRank = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferenceListDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreferenceListDetails_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreferenceListDetails_PreferenceLists_PreferenceListId",
                        column: x => x.PreferenceListId,
                        principalTable: "PreferenceLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistDetailModalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampusId = table.Column<int>(type: "int", nullable: false),
                    ModalityId = table.Column<int>(type: "int", nullable: false),
                    TimeOfDayId = table.Column<int>(type: "int", nullable: true),
                    WishlistDetailId = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistDetailModalities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistDetailModalities_Campuses_CampusId",
                        column: x => x.CampusId,
                        principalTable: "Campuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistDetailModalities_Modalities_ModalityId",
                        column: x => x.ModalityId,
                        principalTable: "Modalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistDetailModalities_TimeOfDays_TimeOfDayId",
                        column: x => x.TimeOfDayId,
                        principalTable: "TimeOfDays",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WishlistDetailModalities_WishlistDetails_WishlistDetailId",
                        column: x => x.WishlistDetailId,
                        principalTable: "WishlistDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreferenceListDetailModalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampusId = table.Column<int>(type: "int", nullable: true),
                    DaysOfWeekId = table.Column<int>(type: "int", nullable: true),
                    ModalityId = table.Column<int>(type: "int", nullable: false),
                    PreferenceListDetailId = table.Column<int>(type: "int", nullable: false),
                    TimeBlockId = table.Column<int>(type: "int", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferenceListDetailModalities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreferenceListDetailModalities_Campuses_CampusId",
                        column: x => x.CampusId,
                        principalTable: "Campuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PreferenceListDetailModalities_DaysOfWeeks_DaysOfWeekId",
                        column: x => x.DaysOfWeekId,
                        principalTable: "DaysOfWeeks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PreferenceListDetailModalities_Modalities_ModalityId",
                        column: x => x.ModalityId,
                        principalTable: "Modalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreferenceListDetailModalities_PreferenceListDetails_PreferenceListDetailId",
                        column: x => x.PreferenceListDetailId,
                        principalTable: "PreferenceListDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreferenceListDetailModalities_TimeBlocks_TimeBlockId",
                        column: x => x.TimeBlockId,
                        principalTable: "TimeBlocks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_StudentId",
                table: "Wishlists",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenceListDetailModalities_CampusId",
                table: "PreferenceListDetailModalities",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenceListDetailModalities_DaysOfWeekId",
                table: "PreferenceListDetailModalities",
                column: "DaysOfWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenceListDetailModalities_ModalityId",
                table: "PreferenceListDetailModalities",
                column: "ModalityId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenceListDetailModalities_PreferenceListDetailId",
                table: "PreferenceListDetailModalities",
                column: "PreferenceListDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenceListDetailModalities_TimeBlockId",
                table: "PreferenceListDetailModalities",
                column: "TimeBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenceListDetails_CourseId",
                table: "PreferenceListDetails",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenceListDetails_PreferenceListId",
                table: "PreferenceListDetails",
                column: "PreferenceListId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenceLists_InstructorId",
                table: "PreferenceLists",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenceLists_SemesterInstanceId",
                table: "PreferenceLists",
                column: "SemesterInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistDetailModalities_CampusId",
                table: "WishlistDetailModalities",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistDetailModalities_ModalityId",
                table: "WishlistDetailModalities",
                column: "ModalityId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistDetailModalities_TimeOfDayId",
                table: "WishlistDetailModalities",
                column: "TimeOfDayId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistDetailModalities_WishlistDetailId",
                table: "WishlistDetailModalities",
                column: "WishlistDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistDetails_CourseId",
                table: "WishlistDetails",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistDetails_WishlistId",
                table: "WishlistDetails",
                column: "WishlistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Students_StudentId",
                table: "Wishlists",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

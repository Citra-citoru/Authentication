using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace authApi.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    userName = table.Column<string>(nullable: false),
                    fullName = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: false),
                    imageUrl = table.Column<string>(nullable: true),
                    isVerified = table.Column<bool>(nullable: false),
                    token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    courseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.id);
                    table.ForeignKey(
                        name: "FK_Section_Course_courseId",
                        column: x => x.courseId,
                        principalTable: "Course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    videoUrl = table.Column<string>(nullable: false),
                    order = table.Column<int>(nullable: false),
                    sectionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.id);
                    table.ForeignKey(
                        name: "FK_Lesson_Section_sectionId",
                        column: x => x.sectionId,
                        principalTable: "Section",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WatchLog",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    courseId = table.Column<Guid>(nullable: false),
                    lessonId = table.Column<Guid>(nullable: false),
                    userId = table.Column<Guid>(nullable: false),
                    percentageWatched = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchLog", x => x.id);
                    table.ForeignKey(
                        name: "FK_WatchLog_Course_courseId",
                        column: x => x.courseId,
                        principalTable: "Course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WatchLog_Lesson_lessonId",
                        column: x => x.lessonId,
                        principalTable: "Lesson",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WatchLog_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_sectionId",
                table: "Lesson",
                column: "sectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_courseId",
                table: "Section",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchLog_courseId",
                table: "WatchLog",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchLog_lessonId",
                table: "WatchLog",
                column: "lessonId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchLog_userId",
                table: "WatchLog",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchLog");

            migrationBuilder.DropTable(
                name: "Lesson");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "Course");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatefkforallentities202408311437 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Lessons_LessonId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Homeworks_HomeworkId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Levels_Courses_CourseId",
                table: "Levels");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Levels_LevelId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Programs_LevelId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Levels_CourseId",
                table: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_HomeworkId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Courses_LessonId",
                table: "Courses");

            migrationBuilder.AlterColumn<Guid>(
                name: "LevelId",
                table: "Programs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseId",
                table: "Levels",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "HomeworkId",
                table: "Lessons",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "LessonId",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_LevelId",
                table: "Programs",
                column: "LevelId",
                unique: true,
                filter: "[LevelId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_CourseId",
                table: "Levels",
                column: "CourseId",
                unique: true,
                filter: "[CourseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_HomeworkId",
                table: "Lessons",
                column: "HomeworkId",
                unique: true,
                filter: "[HomeworkId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LessonId",
                table: "Courses",
                column: "LessonId",
                unique: true,
                filter: "[LessonId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Lessons_LessonId",
                table: "Courses",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Homeworks_HomeworkId",
                table: "Lessons",
                column: "HomeworkId",
                principalTable: "Homeworks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_Courses_CourseId",
                table: "Levels",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Levels_LevelId",
                table: "Programs",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Lessons_LessonId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Homeworks_HomeworkId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Levels_Courses_CourseId",
                table: "Levels");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Levels_LevelId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Programs_LevelId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Levels_CourseId",
                table: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_HomeworkId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Courses_LessonId",
                table: "Courses");

            migrationBuilder.AlterColumn<Guid>(
                name: "LevelId",
                table: "Programs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseId",
                table: "Levels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "HomeworkId",
                table: "Lessons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LessonId",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Programs_LevelId",
                table: "Programs",
                column: "LevelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Levels_CourseId",
                table: "Levels",
                column: "CourseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_HomeworkId",
                table: "Lessons",
                column: "HomeworkId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LessonId",
                table: "Courses",
                column: "LessonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Lessons_LessonId",
                table: "Courses",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Homeworks_HomeworkId",
                table: "Lessons",
                column: "HomeworkId",
                principalTable: "Homeworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_Courses_CourseId",
                table: "Levels",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Levels_LevelId",
                table: "Programs",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class changeisactivedtoisactive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActived",
                table: "Users",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsActived",
                table: "Students",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsActived",
                table: "StudentCourses",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsActived",
                table: "Shifts",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsActived",
                table: "Roles",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsActived",
                table: "Programs",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsActived",
                table: "Parents",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsActived",
                table: "Levels",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsActived",
                table: "LessonTemplates",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsActived",
                table: "Lessons",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsActived",
                table: "Homeworks",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsActived",
                table: "CourseTemplates",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsActived",
                table: "Courses",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsActived",
                table: "Attendances",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsActived",
                table: "Accounts",
                newName: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Users",
                newName: "IsActived");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Students",
                newName: "IsActived");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "StudentCourses",
                newName: "IsActived");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Shifts",
                newName: "IsActived");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Roles",
                newName: "IsActived");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Programs",
                newName: "IsActived");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Parents",
                newName: "IsActived");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Levels",
                newName: "IsActived");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "LessonTemplates",
                newName: "IsActived");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Lessons",
                newName: "IsActived");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Homeworks",
                newName: "IsActived");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "CourseTemplates",
                newName: "IsActived");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Courses",
                newName: "IsActived");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Attendances",
                newName: "IsActived");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Accounts",
                newName: "IsActived");
        }
    }
}

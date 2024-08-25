using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatemenuentity25082024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsView",
                table: "Menu",
                newName: "HasView");

            migrationBuilder.RenameColumn(
                name: "IsStatistic",
                table: "Menu",
                newName: "HasStatistic");

            migrationBuilder.RenameColumn(
                name: "IsEdit",
                table: "Menu",
                newName: "HasEdit");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "Menu",
                newName: "HasDelete");

            migrationBuilder.RenameColumn(
                name: "IsApprove",
                table: "Menu",
                newName: "HasApprove");

            migrationBuilder.RenameColumn(
                name: "IsAdd",
                table: "Menu",
                newName: "HasAdd");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HasView",
                table: "Menu",
                newName: "IsView");

            migrationBuilder.RenameColumn(
                name: "HasStatistic",
                table: "Menu",
                newName: "IsStatistic");

            migrationBuilder.RenameColumn(
                name: "HasEdit",
                table: "Menu",
                newName: "IsEdit");

            migrationBuilder.RenameColumn(
                name: "HasDelete",
                table: "Menu",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "HasApprove",
                table: "Menu",
                newName: "IsApprove");

            migrationBuilder.RenameColumn(
                name: "HasAdd",
                table: "Menu",
                newName: "IsAdd");
        }
    }
}

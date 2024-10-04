using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class changeisactiveforgrouppermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActived",
                table: "Menu",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsActived",
                table: "GroupPermissions",
                newName: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Menu",
                newName: "IsActived");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "GroupPermissions",
                newName: "IsActived");
        }
    }
}

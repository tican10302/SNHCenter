using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateplaceforuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "DistrictCode",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceCode",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WardCode",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DistrictCode",
                table: "Users",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProvinceCode",
                table: "Users",
                column: "ProvinceCode");

            migrationBuilder.CreateIndex(
                name: "IX_Users_WardCode",
                table: "Users",
                column: "WardCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Districts_DistrictCode",
                table: "Users",
                column: "DistrictCode",
                principalTable: "Districts",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Provinces_ProvinceCode",
                table: "Users",
                column: "ProvinceCode",
                principalTable: "Provinces",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Wards_WardCode",
                table: "Users",
                column: "WardCode",
                principalTable: "Wards",
                principalColumn: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Districts_DistrictCode",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Provinces_ProvinceCode",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Wards_WardCode",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DistrictCode",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProvinceCode",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_WardCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DistrictCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProvinceCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WardCode",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatemenuentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupPermissionId",
                table: "Menu",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Menu_GroupPermissionId",
                table: "Menu",
                column: "GroupPermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_GroupPermissions_GroupPermissionId",
                table: "Menu",
                column: "GroupPermissionId",
                principalTable: "GroupPermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_GroupPermissions_GroupPermissionId",
                table: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_Menu_GroupPermissionId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "GroupPermissionId",
                table: "Menu");
        }
    }
}

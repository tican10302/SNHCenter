using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updategrouppermissionandmenu2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermissions_Menu_MenuId",
                table: "GroupPermissions");

            migrationBuilder.DropIndex(
                name: "IX_GroupPermissions_MenuId",
                table: "GroupPermissions");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "GroupPermissions");

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

            migrationBuilder.AddColumn<Guid>(
                name: "MenuId",
                table: "GroupPermissions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupPermissions_MenuId",
                table: "GroupPermissions",
                column: "MenuId",
                unique: true,
                filter: "[MenuId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermissions_Menu_MenuId",
                table: "GroupPermissions",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id");
        }
    }
}

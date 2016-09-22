using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesTracker.Migrations
{
    public partial class DropSaleUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_AspNetUsers_UserId1",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_UserId1",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Sales");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Sales",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_UserId",
                table: "Sales",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_AspNetUsers_UserId",
                table: "Sales",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_AspNetUsers_UserId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_UserId",
                table: "Sales");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Sales",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Sales",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_UserId1",
                table: "Sales",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_AspNetUsers_UserId1",
                table: "Sales",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

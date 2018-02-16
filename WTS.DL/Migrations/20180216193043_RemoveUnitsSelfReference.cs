using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WTS.DL.Migrations
{
    public partial class RemoveUnitsSelfReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Units_UnitId",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_UnitId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Units");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Units",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Units_UnitId",
                table: "Units",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Units_UnitId",
                table: "Units",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

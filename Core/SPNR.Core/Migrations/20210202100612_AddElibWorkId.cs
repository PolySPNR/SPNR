using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPNR.Core.Migrations
{
    public partial class AddElibWorkId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ELibWorkId",
                table: "ELibFields",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "RawMetrics",
                table: "ELibFields",
                type: "jsonb",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ELibWorkId",
                table: "ELibFields");

            migrationBuilder.DropColumn(
                name: "RawMetrics",
                table: "ELibFields");
        }
    }
}

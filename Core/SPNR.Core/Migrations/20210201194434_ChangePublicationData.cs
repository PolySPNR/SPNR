using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPNR.Core.Migrations
{
    public partial class ChangePublicationData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JournalPublishes_ScientificWorkId",
                table: "JournalPublishes");

            migrationBuilder.DropColumn(
                name: "PublicationDate",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "PublishType",
                table: "Works");

            migrationBuilder.AddColumn<byte[]>(
                name: "PublicationInfo",
                table: "Works",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PublicationMeta",
                table: "Works",
                type: "jsonb",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JournalPublishes_ScientificWorkId",
                table: "JournalPublishes",
                column: "ScientificWorkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JournalPublishes_ScientificWorkId",
                table: "JournalPublishes");

            migrationBuilder.DropColumn(
                name: "PublicationInfo",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "PublicationMeta",
                table: "Works");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublicationDate",
                table: "Works",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PublishType",
                table: "Works",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JournalPublishes_ScientificWorkId",
                table: "JournalPublishes",
                column: "ScientificWorkId",
                unique: true);
        }
    }
}

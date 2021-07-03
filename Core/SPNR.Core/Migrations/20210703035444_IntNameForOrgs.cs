using Microsoft.EntityFrameworkCore.Migrations;

namespace SPNR.Core.Migrations
{
    public partial class IntNameForOrgs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IntName",
                table: "Organizations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "OrganizationId",
                keyValue: 1,
                column: "IntName",
                value: "Moscow Polytechnic University");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntName",
                table: "Organizations");
        }
    }
}

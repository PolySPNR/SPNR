using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPNR.Core.Migrations
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrganizationId);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.PositionId);
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    ScientificWorkId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WorkName = table.Column<string>(type: "TEXT", nullable: true),
                    PublicationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PublishType = table.Column<int>(type: "INTEGER", nullable: false),
                    DigitalObjectIdentifier = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.ScientificWorkId);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    FacultyId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    OrganizationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.FacultyId);
                    table.ForeignKey(
                        name: "FK_Faculties_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ELibFields",
                columns: table => new
                {
                    ELibInfoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RINC = table.Column<bool>(type: "INTEGER", nullable: false),
                    RINCCore = table.Column<bool>(type: "INTEGER", nullable: false),
                    Scopus = table.Column<bool>(type: "INTEGER", nullable: false),
                    WebOfScience = table.Column<bool>(type: "INTEGER", nullable: false),
                    ScientificWorkId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ELibFields", x => x.ELibInfoId);
                    table.ForeignKey(
                        name: "FK_ELibFields_Works_ScientificWorkId",
                        column: x => x.ScientificWorkId,
                        principalTable: "Works",
                        principalColumn: "ScientificWorkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JournalPublishes",
                columns: table => new
                {
                    JournalPublishId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ISSN = table.Column<string>(type: "TEXT", nullable: true),
                    Number = table.Column<string>(type: "TEXT", nullable: true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    StartPage = table.Column<int>(type: "INTEGER", nullable: false),
                    EndPage = table.Column<int>(type: "INTEGER", nullable: false),
                    ScientificWorkId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalPublishes", x => x.JournalPublishId);
                    table.ForeignKey(
                        name: "FK_JournalPublishes_Works_ScientificWorkId",
                        column: x => x.ScientificWorkId,
                        principalTable: "Works",
                        principalColumn: "ScientificWorkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    FacultyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                    table.ForeignKey(
                        name: "FK_Departments_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "FacultyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NameEnglish = table.Column<string>(type: "TEXT", nullable: true),
                    Hourly = table.Column<bool>(type: "INTEGER", nullable: false),
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    PositionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                    table.ForeignKey(
                        name: "FK_Authors_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Authors_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorScientificWork",
                columns: table => new
                {
                    AuthorsAuthorId = table.Column<int>(type: "INTEGER", nullable: false),
                    ScientificWorksScientificWorkId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorScientificWork", x => new { x.AuthorsAuthorId, x.ScientificWorksScientificWorkId });
                    table.ForeignKey(
                        name: "FK_AuthorScientificWork_Authors_AuthorsAuthorId",
                        column: x => x.AuthorsAuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorScientificWork_Works_ScientificWorksScientificWorkId",
                        column: x => x.ScientificWorksScientificWorkId,
                        principalTable: "Works",
                        principalColumn: "ScientificWorkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "OrganizationId", "Name" },
                values: new object[] { 1, "Московский Политехнический Университет" });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "PositionId", "Name" },
                values: new object[] { 1, "Доцент" });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "PositionId", "Name" },
                values: new object[] { 2, "Студент" });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "FacultyId", "Name", "OrganizationId" },
                values: new object[] { 1, "Факультет Информационных Технологий", 1 });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "FacultyId", "Name" },
                values: new object[] { 1, 1, "Кафедра Информационной Безопасности" });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_DepartmentId",
                table: "Authors",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_PositionId",
                table: "Authors",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorScientificWork_ScientificWorksScientificWorkId",
                table: "AuthorScientificWork",
                column: "ScientificWorksScientificWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_FacultyId",
                table: "Departments",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_ELibFields_ScientificWorkId",
                table: "ELibFields",
                column: "ScientificWorkId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_Name",
                table: "Faculties",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_OrganizationId",
                table: "Faculties",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalPublishes_ScientificWorkId",
                table: "JournalPublishes",
                column: "ScientificWorkId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_Name",
                table: "Organizations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_Name",
                table: "Positions",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorScientificWork");

            migrationBuilder.DropTable(
                name: "ELibFields");

            migrationBuilder.DropTable(
                name: "JournalPublishes");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}

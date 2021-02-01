using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPNR.Core.Migrations
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Organizations",
                table => new
                {
                    OrganizationId = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Organizations", x => x.OrganizationId); });

            migrationBuilder.CreateTable(
                "Positions",
                table => new
                {
                    PositionId = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Positions", x => x.PositionId); });

            migrationBuilder.CreateTable(
                "Works",
                table => new
                {
                    ScientificWorkId = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WorkName = table.Column<string>("TEXT", nullable: true),
                    PublicationDate = table.Column<DateTime>("TEXT", nullable: false),
                    PublishType = table.Column<int>("INTEGER", nullable: false),
                    DigitalObjectIdentifier = table.Column<string>("TEXT", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Works", x => x.ScientificWorkId); });

            migrationBuilder.CreateTable(
                "Faculties",
                table => new
                {
                    FacultyId = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", nullable: false),
                    OrganizationId = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.FacultyId);
                    table.ForeignKey(
                        "FK_Faculties_Organizations_OrganizationId",
                        x => x.OrganizationId,
                        "Organizations",
                        "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "ELibFields",
                table => new
                {
                    ELibInfoId = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RINC = table.Column<bool>("INTEGER", nullable: false),
                    RINCCore = table.Column<bool>("INTEGER", nullable: false),
                    Scopus = table.Column<bool>("INTEGER", nullable: false),
                    WebOfScience = table.Column<bool>("INTEGER", nullable: false),
                    ScientificWorkId = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ELibFields", x => x.ELibInfoId);
                    table.ForeignKey(
                        "FK_ELibFields_Works_ScientificWorkId",
                        x => x.ScientificWorkId,
                        "Works",
                        "ScientificWorkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "JournalPublishes",
                table => new
                {
                    JournalPublishId = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", nullable: true),
                    ISSN = table.Column<string>("TEXT", nullable: true),
                    Number = table.Column<string>("TEXT", nullable: true),
                    Year = table.Column<int>("INTEGER", nullable: false),
                    StartPage = table.Column<int>("INTEGER", nullable: false),
                    EndPage = table.Column<int>("INTEGER", nullable: false),
                    ScientificWorkId = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalPublishes", x => x.JournalPublishId);
                    table.ForeignKey(
                        "FK_JournalPublishes_Works_ScientificWorkId",
                        x => x.ScientificWorkId,
                        "Works",
                        "ScientificWorkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Departments",
                table => new
                {
                    DepartmentId = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", nullable: false),
                    FacultyId = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                    table.ForeignKey(
                        "FK_Departments_Faculties_FacultyId",
                        x => x.FacultyId,
                        "Faculties",
                        "FacultyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Authors",
                table => new
                {
                    AuthorId = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", nullable: false),
                    NameEnglish = table.Column<string>("TEXT", nullable: true),
                    Hourly = table.Column<bool>("INTEGER", nullable: false),
                    OrganizationId = table.Column<int>("INTEGER", nullable: false),
                    FacultyId = table.Column<int>("INTEGER", nullable: false),
                    DepartmentId = table.Column<int>("INTEGER", nullable: false),
                    PositionId = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                    table.ForeignKey(
                        "FK_Authors_Departments_DepartmentId",
                        x => x.DepartmentId,
                        "Departments",
                        "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Authors_Faculties_FacultyId",
                        x => x.FacultyId,
                        "Faculties",
                        "FacultyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Authors_Organizations_OrganizationId",
                        x => x.OrganizationId,
                        "Organizations",
                        "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Authors_Positions_PositionId",
                        x => x.PositionId,
                        "Positions",
                        "PositionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AuthorScientificWork",
                table => new
                {
                    AuthorsAuthorId = table.Column<int>("INTEGER", nullable: false),
                    ScientificWorksScientificWorkId = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorScientificWork",
                        x => new {x.AuthorsAuthorId, x.ScientificWorksScientificWorkId});
                    table.ForeignKey(
                        "FK_AuthorScientificWork_Authors_AuthorsAuthorId",
                        x => x.AuthorsAuthorId,
                        "Authors",
                        "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_AuthorScientificWork_Works_ScientificWorksScientificWorkId",
                        x => x.ScientificWorksScientificWorkId,
                        "Works",
                        "ScientificWorkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                "Organizations",
                new[] {"OrganizationId", "Name"},
                new object[] {1, "Московский Политехнический Университет"});

            migrationBuilder.InsertData(
                "Positions",
                new[] {"PositionId", "Name"},
                new object[] {1, "доцент"});

            migrationBuilder.InsertData(
                "Positions",
                new[] {"PositionId", "Name"},
                new object[] {2, "студент"});

            migrationBuilder.InsertData(
                "Faculties",
                new[] {"FacultyId", "Name", "OrganizationId"},
                new object[] {1, "Факультет Информационных Технологий", 1});

            migrationBuilder.InsertData(
                "Departments",
                new[] {"DepartmentId", "FacultyId", "Name"},
                new object[] {1, 1, "Кафедра Информационной Безопасности"});

            migrationBuilder.CreateIndex(
                "IX_Authors_DepartmentId",
                "Authors",
                "DepartmentId");

            migrationBuilder.CreateIndex(
                "IX_Authors_FacultyId",
                "Authors",
                "FacultyId");

            migrationBuilder.CreateIndex(
                "IX_Authors_OrganizationId",
                "Authors",
                "OrganizationId");

            migrationBuilder.CreateIndex(
                "IX_Authors_PositionId",
                "Authors",
                "PositionId");

            migrationBuilder.CreateIndex(
                "IX_AuthorScientificWork_ScientificWorksScientificWorkId",
                "AuthorScientificWork",
                "ScientificWorksScientificWorkId");

            migrationBuilder.CreateIndex(
                "IX_Departments_FacultyId",
                "Departments",
                "FacultyId");

            migrationBuilder.CreateIndex(
                "IX_ELibFields_ScientificWorkId",
                "ELibFields",
                "ScientificWorkId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Faculties_Name",
                "Faculties",
                "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Faculties_OrganizationId",
                "Faculties",
                "OrganizationId");

            migrationBuilder.CreateIndex(
                "IX_JournalPublishes_ScientificWorkId",
                "JournalPublishes",
                "ScientificWorkId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Organizations_Name",
                "Organizations",
                "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Positions_Name",
                "Positions",
                "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "AuthorScientificWork");

            migrationBuilder.DropTable(
                "ELibFields");

            migrationBuilder.DropTable(
                "JournalPublishes");

            migrationBuilder.DropTable(
                "Authors");

            migrationBuilder.DropTable(
                "Works");

            migrationBuilder.DropTable(
                "Departments");

            migrationBuilder.DropTable(
                "Positions");

            migrationBuilder.DropTable(
                "Faculties");

            migrationBuilder.DropTable(
                "Organizations");
        }
    }
}
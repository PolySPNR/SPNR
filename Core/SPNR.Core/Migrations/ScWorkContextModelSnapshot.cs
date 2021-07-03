﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SPNR.Core.Services.Data.Contexts;

namespace SPNR.Core.Migrations
{
    [DbContext(typeof(ScWorkContext))]
    partial class ScWorkContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("AuthorScientificWork", b =>
                {
                    b.Property<int>("AuthorsAuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ScientificWorksScientificWorkId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AuthorsAuthorId", "ScientificWorksScientificWorkId");

                    b.HasIndex("ScientificWorksScientificWorkId");

                    b.ToTable("AuthorScientificWork");
                });

            modelBuilder.Entity("SPNR.Core.Models.AuthorInfo.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FacultyId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Hourly")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NameEnglish")
                        .HasColumnType("TEXT");

                    b.Property<int>("OrganizationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PositionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AuthorId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("FacultyId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("PositionId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("SPNR.Core.Models.AuthorInfo.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FacultyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("DepartmentId");

                    b.HasIndex("FacultyId");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            DepartmentId = 1,
                            FacultyId = 1,
                            Name = "Кафедра Информационной Безопасности"
                        });
                });

            modelBuilder.Entity("SPNR.Core.Models.AuthorInfo.Faculty", b =>
                {
                    b.Property<int>("FacultyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("OrganizationId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FacultyId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("OrganizationId");

                    b.ToTable("Faculties");

                    b.HasData(
                        new
                        {
                            FacultyId = 1,
                            Name = "Факультет Информационных Технологий",
                            OrganizationId = 1
                        });
                });

            modelBuilder.Entity("SPNR.Core.Models.AuthorInfo.Organization", b =>
                {
                    b.Property<int>("OrganizationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("IntName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("OrganizationId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Organizations");

                    b.HasData(
                        new
                        {
                            OrganizationId = 1,
                            IntName = "Moscow Polytechnic University",
                            Name = "Московский Политехнический Университет"
                        });
                });

            modelBuilder.Entity("SPNR.Core.Models.AuthorInfo.Position", b =>
                {
                    b.Property<int>("PositionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PositionId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Positions");

                    b.HasData(
                        new
                        {
                            PositionId = 1,
                            Name = "доцент"
                        },
                        new
                        {
                            PositionId = 2,
                            Name = "студент"
                        });
                });

            modelBuilder.Entity("SPNR.Core.Models.Works.Fields.ELibInfo", b =>
                {
                    b.Property<int>("ELibInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ELibWorkId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("RINC")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("RINCCore")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RawMetrics")
                        .HasColumnType("jsonb");

                    b.Property<int>("ScientificWorkId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Scopus")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("WebOfScience")
                        .HasColumnType("INTEGER");

                    b.HasKey("ELibInfoId");

                    b.HasIndex("ScientificWorkId")
                        .IsUnique();

                    b.ToTable("ELibFields");
                });

            modelBuilder.Entity("SPNR.Core.Models.Works.PublishData.JournalPublish", b =>
                {
                    b.Property<int>("JournalPublishId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EndPage")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ISSN")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Number")
                        .HasColumnType("TEXT");

                    b.Property<int>("ScientificWorkId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StartPage")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("JournalPublishId");

                    b.HasIndex("ScientificWorkId");

                    b.ToTable("JournalPublishes");
                });

            modelBuilder.Entity("SPNR.Core.Models.Works.ScientificWork", b =>
                {
                    b.Property<int>("ScientificWorkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DigitalObjectIdentifier")
                        .HasColumnType("TEXT");

                    b.Property<string>("PublicationInfo")
                        .HasColumnType("jsonb");

                    b.Property<string>("PublicationMeta")
                        .HasColumnType("jsonb");

                    b.Property<string>("WorkName")
                        .HasColumnType("TEXT");

                    b.HasKey("ScientificWorkId");

                    b.ToTable("Works");
                });

            modelBuilder.Entity("AuthorScientificWork", b =>
                {
                    b.HasOne("SPNR.Core.Models.AuthorInfo.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorsAuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPNR.Core.Models.Works.ScientificWork", null)
                        .WithMany()
                        .HasForeignKey("ScientificWorksScientificWorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SPNR.Core.Models.AuthorInfo.Author", b =>
                {
                    b.HasOne("SPNR.Core.Models.AuthorInfo.Department", "Department")
                        .WithMany("Authors")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPNR.Core.Models.AuthorInfo.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPNR.Core.Models.AuthorInfo.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPNR.Core.Models.AuthorInfo.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Faculty");

                    b.Navigation("Organization");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("SPNR.Core.Models.AuthorInfo.Department", b =>
                {
                    b.HasOne("SPNR.Core.Models.AuthorInfo.Faculty", "Faculty")
                        .WithMany("Departments")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("SPNR.Core.Models.AuthorInfo.Faculty", b =>
                {
                    b.HasOne("SPNR.Core.Models.AuthorInfo.Organization", "Organization")
                        .WithMany("Faculties")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("SPNR.Core.Models.Works.Fields.ELibInfo", b =>
                {
                    b.HasOne("SPNR.Core.Models.Works.ScientificWork", "ScientificWork")
                        .WithOne("ELibInfo")
                        .HasForeignKey("SPNR.Core.Models.Works.Fields.ELibInfo", "ScientificWorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ScientificWork");
                });

            modelBuilder.Entity("SPNR.Core.Models.Works.PublishData.JournalPublish", b =>
                {
                    b.HasOne("SPNR.Core.Models.Works.ScientificWork", "ScientificWork")
                        .WithMany()
                        .HasForeignKey("ScientificWorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ScientificWork");
                });

            modelBuilder.Entity("SPNR.Core.Models.AuthorInfo.Department", b =>
                {
                    b.Navigation("Authors");
                });

            modelBuilder.Entity("SPNR.Core.Models.AuthorInfo.Faculty", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("SPNR.Core.Models.AuthorInfo.Organization", b =>
                {
                    b.Navigation("Faculties");
                });

            modelBuilder.Entity("SPNR.Core.Models.Works.ScientificWork", b =>
                {
                    b.Navigation("ELibInfo");
                });
#pragma warning restore 612, 618
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospi.App.Persistence.Migrations
{
    public partial class Intit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Diagnosis = table.Column<string>(nullable: true),
                    Enviroment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CareSuggestions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    HistoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareSuggestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CareSuggestions_Histories_HistoryId",
                        column: x => x.HistoryId,
                        principalTable: "Histories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Surname = table.Column<string>(maxLength: 50, nullable: false),
                    Cellphone = table.Column<string>(maxLength: 50, nullable: false),
                    Genre = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    MedicalSpecialty = table.Column<string>(maxLength: 50, nullable: true),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    RethusRecord = table.Column<string>(maxLength: 50, nullable: true),
                    ProfessionalCard = table.Column<string>(maxLength: 50, nullable: true),
                    WorkingHours = table.Column<int>(nullable: true),
                    HistoryId = table.Column<int>(nullable: true),
                    RelativeId = table.Column<int>(nullable: true),
                    NurseId = table.Column<int>(nullable: true),
                    DoctorId = table.Column<int>(nullable: true),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    Relationship = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_People_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_Histories_HistoryId",
                        column: x => x.HistoryId,
                        principalTable: "Histories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_People_NurseId",
                        column: x => x.NurseId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_People_RelativeId",
                        column: x => x.RelativeId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VitalSigns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    Sign = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VitalSigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VitalSigns_People_PatientId",
                        column: x => x.PatientId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CareSuggestions_HistoryId",
                table: "CareSuggestions",
                column: "HistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_People_DoctorId",
                table: "People",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_People_HistoryId",
                table: "People",
                column: "HistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_People_NurseId",
                table: "People",
                column: "NurseId");

            migrationBuilder.CreateIndex(
                name: "IX_People_RelativeId",
                table: "People",
                column: "RelativeId");

            migrationBuilder.CreateIndex(
                name: "IX_VitalSigns_PatientId",
                table: "VitalSigns",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CareSuggestions");

            migrationBuilder.DropTable(
                name: "VitalSigns");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Histories");
        }
    }
}

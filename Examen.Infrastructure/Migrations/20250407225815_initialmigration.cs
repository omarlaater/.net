using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.Infrastructure.Migrations
{
   
    public partial class InitialMigration : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Infirmiers",
                columns: table => new
                {
                    CodeInfirmier = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Infirmiers", x => x.CodeInfirmier);
                });

            migrationBuilder.CreateTable(
                name: "Laboratoires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdresseLabo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratoires", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    CodePatient = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Informations = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.CodePatient);
                });

            migrationBuilder.CreateTable(
                name: "Bilans",
                columns: table => new
                {
                    CodeInfirmier = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CodePatient = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    DatePrelevement = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bilans", x => new { x.CodeInfirmier, x.CodePatient, x.DatePrelevement });
                    table.ForeignKey(
                        name: "FK_Bilans_Infirmiers_CodeInfirmier",
                        column: x => x.CodeInfirmier,
                        principalTable: "Infirmiers",
                        principalColumn: "CodeInfirmier",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bilans_Patients_CodePatient",
                        column: x => x.CodePatient,
                        principalTable: "Patients",
                        principalColumn: "CodePatient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Analyses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DureeResultat = table.Column<int>(type: "int", nullable: false),
                    ValeurAnalyse = table.Column<float>(type: "real", nullable: false),
                    ValeurMinNormale = table.Column<float>(type: "real", nullable: false),
                    ValeurMaxNormale = table.Column<float>(type: "real", nullable: false),
                    LaboratoireId = table.Column<int>(type: "int", nullable: true),
                    BilanId = table.Column<int>(type: "int", nullable: false),
                    BilanCodeInfirmier = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BilanCodePatient = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    BilanDatePrelevement = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analyses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Analyses_Bilans_BilanCodeInfirmier_BilanCodePatient_BilanDatePrelevement",
                        columns: x => new { x.BilanCodeInfirmier, x.BilanCodePatient, x.BilanDatePrelevement },
                        principalTable: "Bilans",
                        principalColumns: new[] { "CodeInfirmier", "CodePatient", "DatePrelevement" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Analyses_Laboratoires_LaboratoireId",
                        column: x => x.LaboratoireId,
                        principalTable: "Laboratoires",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analyses_BilanCodeInfirmier_BilanCodePatient_BilanDatePrelevement",
                table: "Analyses",
                columns: new[] { "BilanCodeInfirmier", "BilanCodePatient", "BilanDatePrelevement" });

            migrationBuilder.CreateIndex(
                name: "IX_Analyses_LaboratoireId",
                table: "Analyses",
                column: "LaboratoireId");

            migrationBuilder.CreateIndex(
                name: "IX_Bilans_CodePatient",
                table: "Bilans",
                column: "CodePatient");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analyses");

            migrationBuilder.DropTable(
                name: "Bilans");

            migrationBuilder.DropTable(
                name: "Laboratoires");

            migrationBuilder.DropTable(
                name: "Infirmiers");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}

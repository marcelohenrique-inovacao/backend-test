using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace backendtest.Domain.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Desenvolvedor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "VarChar(255)", nullable: false),
                    CPF = table.Column<string>(type: "Char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: true),
                    Email = table.Column<string>(type: "VarChar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desenvolvedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Aplicativo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "VarChar(255)", nullable: false),
                    DataLancamento = table.Column<DateTime>(type: "SmallDatetime", nullable: false),
                    Plataforma = table.Column<byte>(type: "TinyInt", nullable: false),
                    Id_DesenvolvedorResponsavel = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aplicativo", x => x.Id);
                    table.ForeignKey(
                        name: "Id_DesenvolvedorResponsavel",
                        column: x => x.Id_DesenvolvedorResponsavel,
                        principalTable: "Desenvolvedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DesenvolvedorAplicativo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_Desenvolvedor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_Aplicativo = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesenvolvedorAplicativo", x => x.Id);
                    table.ForeignKey(
                        name: "Id_Aplicativo",
                        column: x => x.Id_Aplicativo,
                        principalTable: "Aplicativo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Id_Desenvolvedor",
                        column: x => x.Id_Desenvolvedor,
                        principalTable: "Desenvolvedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "Idx_Id_DesenvolvedorResponsavel",
                table: "Aplicativo",
                column: "Id_DesenvolvedorResponsavel");

            migrationBuilder.CreateIndex(
                name: "Idx_Id_Aplicativo",
                table: "DesenvolvedorAplicativo",
                column: "Id_Aplicativo");

            migrationBuilder.CreateIndex(
                name: "Idx_Id_Desenvolvedor",
                table: "DesenvolvedorAplicativo",
                column: "Id_Desenvolvedor");

            migrationBuilder.CreateIndex(
                name: "Idx_Id_Desenvolvedor_Id_Aplicativo",
                table: "DesenvolvedorAplicativo",
                columns: new[] { "Id_Desenvolvedor", "Id_Aplicativo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesenvolvedorAplicativo");

            migrationBuilder.DropTable(
                name: "Aplicativo");

            migrationBuilder.DropTable(
                name: "Desenvolvedor");
        }
    }
}

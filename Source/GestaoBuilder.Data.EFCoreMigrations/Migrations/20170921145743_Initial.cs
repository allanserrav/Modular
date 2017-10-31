using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GestaoBuilder.Data.EFCoreMigrations.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "empresa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(maxLength: 20, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IsDesabilitado = table.Column<bool>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "modulo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Categoria = table.Column<string>(maxLength: 200, nullable: false),
                    Codigo = table.Column<string>(maxLength: 20, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    IsAgrupamento = table.Column<bool>(nullable: false),
                    IsDesabilitado = table.Column<bool>(nullable: false),
                    IsObsoleto = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Assembly = table.Column<string>(maxLength: 1000, nullable: true),
                    AssemblyFullPath = table.Column<string>(maxLength: 1000, nullable: true),
                    ScriptMethod = table.Column<string>(maxLength: 20, nullable: true),
                    ScriptResourceId = table.Column<int>(nullable: true),
                    ScriptTipo = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modulo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(maxLength: 20, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IsDesabilitado = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    SegundosLoginExpirar = table.Column<int>(nullable: false),
                    Senha = table.Column<string>(maxLength: 10, nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "modulo_ordem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IsDesabilitado = table.Column<bool>(nullable: false),
                    IsPrincipal = table.Column<bool>(nullable: false),
                    ModuloAgrupamentoId = table.Column<int>(nullable: true),
                    ModuloExecutorId = table.Column<int>(nullable: true),
                    Ordem = table.Column<int>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modulo_ordem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_modulo_ordem_modulo_ModuloAgrupamentoId",
                        column: x => x.ModuloAgrupamentoId,
                        principalTable: "modulo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_modulo_ordem_modulo_ModuloExecutorId",
                        column: x => x.ModuloExecutorId,
                        principalTable: "modulo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_empresa_Codigo",
                table: "empresa",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_modulo_Codigo",
                table: "modulo",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_modulo_ordem_ModuloAgrupamentoId",
                table: "modulo_ordem",
                column: "ModuloAgrupamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_modulo_ordem_ModuloExecutorId",
                table: "modulo_ordem",
                column: "ModuloExecutorId");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_Codigo",
                table: "usuario",
                column: "Codigo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "empresa");

            migrationBuilder.DropTable(
                name: "modulo_ordem");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "modulo");
        }
    }
}

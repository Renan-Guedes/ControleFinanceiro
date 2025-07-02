using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ControleFinanceiro.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoTransacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoTransacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banco_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categoria_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanejamentoMensal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Mes = table.Column<int>(type: "int", nullable: false),
                    SaldoInicial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BancoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanejamentoMensal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanejamentoMensal_Banco_BancoId",
                        column: x => x.BancoId,
                        principalTable: "Banco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanejamentoMensal_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fatura = table.Column<bool>(type: "bit", nullable: false),
                    ValorPlanejado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorPago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    TipoTransacaoId = table.Column<int>(type: "int", nullable: false),
                    BancoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataTransacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacao_Banco_BancoId",
                        column: x => x.BancoId,
                        principalTable: "Banco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transacao_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transacao_TipoTransacao_TipoTransacaoId",
                        column: x => x.TipoTransacaoId,
                        principalTable: "TipoTransacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transacao_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GastoFixo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    TipoTransacaoId = table.Column<int>(type: "int", nullable: false),
                    BancoId = table.Column<int>(type: "int", nullable: false),
                    PlanjamentoMensalId = table.Column<int>(type: "int", nullable: false),
                    PlanejamentoMensalId = table.Column<int>(type: "int", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GastoFixo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GastoFixo_Banco_BancoId",
                        column: x => x.BancoId,
                        principalTable: "Banco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GastoFixo_PlanejamentoMensal_PlanejamentoMensalId",
                        column: x => x.PlanejamentoMensalId,
                        principalTable: "PlanejamentoMensal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GastoFixo_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TipoTransacao",
                columns: new[] { "Id", "DataAtualizacao", "DataExclusao", "DataInclusao", "Nome" },
                values: new object[,]
                {
                    { 1, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receita" },
                    { 2, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Despesa" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banco_UsuarioId",
                table: "Banco",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_UsuarioId",
                table: "Categoria",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_GastoFixo_BancoId",
                table: "GastoFixo",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_GastoFixo_PlanejamentoMensalId",
                table: "GastoFixo",
                column: "PlanejamentoMensalId");

            migrationBuilder.CreateIndex(
                name: "IX_GastoFixo_UsuarioId",
                table: "GastoFixo",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanejamentoMensal_BancoId",
                table: "PlanejamentoMensal",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanejamentoMensal_UsuarioId",
                table: "PlanejamentoMensal",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_BancoId",
                table: "Transacao",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_CategoriaId",
                table: "Transacao",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_TipoTransacaoId",
                table: "Transacao",
                column: "TipoTransacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_UsuarioId",
                table: "Transacao",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GastoFixo");

            migrationBuilder.DropTable(
                name: "Transacao");

            migrationBuilder.DropTable(
                name: "PlanejamentoMensal");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "TipoTransacao");

            migrationBuilder.DropTable(
                name: "Banco");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}

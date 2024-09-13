using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio_teste.Migrations
{
    public partial class MigrationFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteID);
                });

            migrationBuilder.CreateTable(
                name: "Concessionarias",
                columns: table => new
                {
                    ConcessionariaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EnderecoCompleto = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CapacidadeMax = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concessionarias", x => x.ConcessionariaID);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NomeUsuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NivelAcesso = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioID);
                });

            migrationBuilder.CreateTable(
                name: "Fabricantes",
                columns: table => new
                {
                    FabricanteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeFabricante = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PaisOrigem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnoFundacao = table.Column<int>(type: "int", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ConcessionariasModelConcessionariaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fabricantes", x => x.FabricanteID);
                    table.ForeignKey(
                        name: "FK_Fabricantes_Concessionarias_ConcessionariasModelConcessionariaID",
                        column: x => x.ConcessionariasModelConcessionariaID,
                        principalTable: "Concessionarias",
                        principalColumn: "ConcessionariaID");
                });

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    VeiculoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModeloVeiculo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AnoFabricacao = table.Column<int>(type: "int", nullable: false),
                    ValorVeiculo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FabricanteID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ConcessionariasModelConcessionariaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.VeiculoID);
                    table.ForeignKey(
                        name: "FK_Veiculos_Concessionarias_ConcessionariasModelConcessionariaID",
                        column: x => x.ConcessionariasModelConcessionariaID,
                        principalTable: "Concessionarias",
                        principalColumn: "ConcessionariaID");
                    table.ForeignKey(
                        name: "FK_Veiculos_Fabricantes_FabricanteID",
                        column: x => x.FabricanteID,
                        principalTable: "Fabricantes",
                        principalColumn: "FabricanteID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vendas",
                columns: table => new
                {
                    VendaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteID = table.Column<int>(type: "int", nullable: false),
                    DataVenda = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProtocoloVenda = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    ConcessionariaID = table.Column<int>(type: "int", nullable: false),
                    FabricanteID = table.Column<int>(type: "int", nullable: false),
                    VeiculoID = table.Column<int>(type: "int", nullable: false),
                    ClientesModelClienteID = table.Column<int>(type: "int", nullable: true),
                    ConcessionariasModelConcessionariaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendas", x => x.VendaId);
                    table.ForeignKey(
                        name: "FK_Vendas_Clientes_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "Clientes",
                        principalColumn: "ClienteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendas_Clientes_ClientesModelClienteID",
                        column: x => x.ClientesModelClienteID,
                        principalTable: "Clientes",
                        principalColumn: "ClienteID");
                    table.ForeignKey(
                        name: "FK_Vendas_Concessionarias_ConcessionariaID",
                        column: x => x.ConcessionariaID,
                        principalTable: "Concessionarias",
                        principalColumn: "ConcessionariaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendas_Concessionarias_ConcessionariasModelConcessionariaID",
                        column: x => x.ConcessionariasModelConcessionariaID,
                        principalTable: "Concessionarias",
                        principalColumn: "ConcessionariaID");
                    table.ForeignKey(
                        name: "FK_Vendas_Fabricantes_FabricanteID",
                        column: x => x.FabricanteID,
                        principalTable: "Fabricantes",
                        principalColumn: "FabricanteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendas_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendas_Veiculos_VeiculoID",
                        column: x => x.VeiculoID,
                        principalTable: "Veiculos",
                        principalColumn: "VeiculoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fabricantes_ConcessionariasModelConcessionariaID",
                table: "Fabricantes",
                column: "ConcessionariasModelConcessionariaID");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_ConcessionariasModelConcessionariaID",
                table: "Veiculos",
                column: "ConcessionariasModelConcessionariaID");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_FabricanteID",
                table: "Veiculos",
                column: "FabricanteID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_ClienteID",
                table: "Vendas",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_ClientesModelClienteID",
                table: "Vendas",
                column: "ClientesModelClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_ConcessionariaID",
                table: "Vendas",
                column: "ConcessionariaID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_ConcessionariasModelConcessionariaID",
                table: "Vendas",
                column: "ConcessionariasModelConcessionariaID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_FabricanteID",
                table: "Vendas",
                column: "FabricanteID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_UsuarioID",
                table: "Vendas",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_VeiculoID",
                table: "Vendas",
                column: "VeiculoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vendas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Veiculos");

            migrationBuilder.DropTable(
                name: "Fabricantes");

            migrationBuilder.DropTable(
                name: "Concessionarias");
        }
    }
}

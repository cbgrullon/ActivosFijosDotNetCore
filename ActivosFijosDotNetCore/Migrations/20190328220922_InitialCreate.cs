using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ActivosFijosDotNetCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 1, nullable: false),
                    Descripcion = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    IdEstado = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departamento_Estado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoActivo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    CuentaCompra = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    CuentaDepreciacion = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    IdEstado = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoActivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TipoActivo_Estado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Cedula = table.Column<string>(unicode: false, maxLength: 11, nullable: false),
                    IdDepartamento = table.Column<int>(nullable: false),
                    TipoPersona = table.Column<string>(unicode: false, maxLength: 1, nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "date", nullable: false),
                    IdEstado = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empleado_Departamento",
                        column: x => x.IdDepartamento,
                        principalTable: "Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Empleado_Estado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivosFijos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    IdDepartamento = table.Column<int>(nullable: false),
                    IdTipoActivo = table.Column<int>(nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: false),
                    ValorCompra = table.Column<decimal>(nullable: false),
                    DepreciacionAcumulada = table.Column<decimal>(nullable: false),
                    IdEstado = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivosFijos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivoFijo_Departamento",
                        column: x => x.IdDepartamento,
                        principalTable: "Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivoFijo_Estado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivoFijo_TipoActivo",
                        column: x => x.IdTipoActivo,
                        principalTable: "TipoActivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalculoDepreciacion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaProceso = table.Column<DateTime>(type: "date", nullable: false),
                    IdActivoFijo = table.Column<int>(nullable: false),
                    MontoDepreciado = table.Column<decimal>(nullable: false),
                    DepreciacionAcumulada = table.Column<decimal>(nullable: false),
                    CuentaCompra = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    CuentaDepreciacion = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculoDepreciacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalculoDepreciacion_ActivoFijo",
                        column: x => x.IdActivoFijo,
                        principalTable: "ActivosFijos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivosFijos_IdDepartamento",
                table: "ActivosFijos",
                column: "IdDepartamento");

            migrationBuilder.CreateIndex(
                name: "IX_ActivosFijos_IdEstado",
                table: "ActivosFijos",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_ActivosFijos_IdTipoActivo",
                table: "ActivosFijos",
                column: "IdTipoActivo");

            migrationBuilder.CreateIndex(
                name: "IX_CalculoDepreciacion_IdActivoFijo",
                table: "CalculoDepreciacion",
                column: "IdActivoFijo");

            migrationBuilder.CreateIndex(
                name: "IX_Departamento_IdEstado",
                table: "Departamento",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_IdDepartamento",
                table: "Empleado",
                column: "IdDepartamento");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_IdEstado",
                table: "Empleado",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_TipoActivo_IdEstado",
                table: "TipoActivo",
                column: "IdEstado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculoDepreciacion");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "ActivosFijos");

            migrationBuilder.DropTable(
                name: "Departamento");

            migrationBuilder.DropTable(
                name: "TipoActivo");

            migrationBuilder.DropTable(
                name: "Estado");
        }
    }
}

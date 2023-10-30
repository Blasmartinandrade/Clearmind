using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Clearmind.Migrations
{
    /// <inheritdoc />
    public partial class _01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "Proyectos",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "nchar(50)", nullable: false),
                    Descripcion = table.Column<string>(type: "nchar(200)", nullable: false),
                    Imagen = table.Column<string>(type: "nchar(120)", nullable: false),
                    Token = table.Column<string>(type: "nchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "nchar(50)", nullable: false),
                    Password = table.Column<string>(type: "nchar(270)", nullable: false),
                    Imagen = table.Column<string>(type: "nchar(120)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosProyectos",
                schema: "public",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(type: "integer", nullable: false),
                    ProyectoID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosProyectos", x => new { x.UsuarioID, x.ProyectoID });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Proyectos",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Usuarios",
                schema: "public");

            migrationBuilder.DropTable(
                name: "UsuariosProyectos",
                schema: "public");
        }
    }
}

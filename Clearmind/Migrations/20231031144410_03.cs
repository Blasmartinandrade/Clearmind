using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clearmind.Migrations
{
    /// <inheritdoc />
    public partial class _03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                schema: "public",
                table: "Usuarios",
                type: "nchar(250)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "public",
                table: "Usuarios",
                type: "nchar(100)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                schema: "public",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "public",
                table: "Usuarios");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appSacoleiro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarMidiasProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "imagem_content_type",
                table: "produtos",
                newName: "video_content_type");

            migrationBuilder.RenameColumn(
                name: "imagem",
                table: "produtos",
                newName: "video");

            migrationBuilder.AddColumn<byte[]>(
                name: "imagem1",
                table: "produtos",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "imagem1_content_type",
                table: "produtos",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "imagem2",
                table: "produtos",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "imagem2_content_type",
                table: "produtos",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagem1",
                table: "produtos");

            migrationBuilder.DropColumn(
                name: "imagem1_content_type",
                table: "produtos");

            migrationBuilder.DropColumn(
                name: "imagem2",
                table: "produtos");

            migrationBuilder.DropColumn(
                name: "imagem2_content_type",
                table: "produtos");

            migrationBuilder.RenameColumn(
                name: "video_content_type",
                table: "produtos",
                newName: "imagem_content_type");

            migrationBuilder.RenameColumn(
                name: "video",
                table: "produtos",
                newName: "imagem");
        }
    }
}

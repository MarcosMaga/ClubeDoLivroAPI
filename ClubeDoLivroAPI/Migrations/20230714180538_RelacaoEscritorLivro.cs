using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubeDoLivroAPI.Migrations
{
    /// <inheritdoc />
    public partial class RelacaoEscritorLivro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EscritorId",
                table: "Livros",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Escritores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escritores", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livros_EscritorId",
                table: "Livros",
                column: "EscritorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Escritores_EscritorId",
                table: "Livros",
                column: "EscritorId",
                principalTable: "Escritores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Escritores_EscritorId",
                table: "Livros");

            migrationBuilder.DropTable(
                name: "Escritores");

            migrationBuilder.DropIndex(
                name: "IX_Livros_EscritorId",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "EscritorId",
                table: "Livros");
        }
    }
}

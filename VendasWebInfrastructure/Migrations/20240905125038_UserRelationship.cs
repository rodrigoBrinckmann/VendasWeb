using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendasWebInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_UserId",
                table: "Produtos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_UserId",
                table: "Pedidos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Users_UserId",
                table: "Pedidos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Users_UserId",
                table: "Produtos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Users_UserId",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Users_UserId",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_UserId",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_UserId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pedidos");
        }
    }
}

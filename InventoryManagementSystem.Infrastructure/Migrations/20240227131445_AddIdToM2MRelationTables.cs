using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIdToM2MRelationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductItems_ProductsInventories_ProductsInventoryProductId_ProductsInventoryInventoryId",
                table: "ProductItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsInventories",
                table: "ProductsInventories");

            migrationBuilder.DropIndex(
                name: "IX_ProductItems_ProductsInventoryProductId_ProductsInventoryInventoryId",
                table: "ProductItems");

            migrationBuilder.DropColumn(
                name: "ProductsInventoryInventoryId",
                table: "ProductItems");

            migrationBuilder.RenameColumn(
                name: "ProductsInventoryProductId",
                table: "ProductItems",
                newName: "ProductsInventoryId");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "ProductsInventories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsInventories",
                table: "ProductsInventories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInventories_ProductId",
                table: "ProductsInventories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_ProductsInventoryId",
                table: "ProductItems",
                column: "ProductsInventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItems_ProductsInventories_ProductsInventoryId",
                table: "ProductItems",
                column: "ProductsInventoryId",
                principalTable: "ProductsInventories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductItems_ProductsInventories_ProductsInventoryId",
                table: "ProductItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsInventories",
                table: "ProductsInventories");

            migrationBuilder.DropIndex(
                name: "IX_ProductsInventories_ProductId",
                table: "ProductsInventories");

            migrationBuilder.DropIndex(
                name: "IX_ProductItems_ProductsInventoryId",
                table: "ProductItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductsInventories");

            migrationBuilder.RenameColumn(
                name: "ProductsInventoryId",
                table: "ProductItems",
                newName: "ProductsInventoryProductId");

            migrationBuilder.AddColumn<string>(
                name: "ProductsInventoryInventoryId",
                table: "ProductItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsInventories",
                table: "ProductsInventories",
                columns: new[] { "ProductId", "InventoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_ProductsInventoryProductId_ProductsInventoryInventoryId",
                table: "ProductItems",
                columns: new[] { "ProductsInventoryProductId", "ProductsInventoryInventoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItems_ProductsInventories_ProductsInventoryProductId_ProductsInventoryInventoryId",
                table: "ProductItems",
                columns: new[] { "ProductsInventoryProductId", "ProductsInventoryInventoryId" },
                principalTable: "ProductsInventories",
                principalColumns: new[] { "ProductId", "InventoryId" });
        }
    }
}

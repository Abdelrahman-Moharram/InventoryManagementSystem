﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductAmountColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalAmount",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Orders");
        }
    }
}

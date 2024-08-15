using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expendio.Migrations
{
    /// <inheritdoc />
    public partial class dateAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "date",
                table: "Incomes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "date",
                table: "Expenses",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "date",
                table: "Expenses");
        }
    }
}

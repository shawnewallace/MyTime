using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTime.Api.Migrations;

/// <inheritdoc />
public partial class createCategory : Migration
{
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.CreateTable(
        name: "Categories",
        columns: table => new
        {
          Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
          Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
          WhenCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
          WhenUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
          IsDeleted = table.Column<bool>(type: "bit", nullable: false)
        },
        constraints: table => table.PrimaryKey("PK_Categories", x => x.Id));

    migrationBuilder.CreateIndex(
        name: "IX_Categories_Name",
        table: "Categories",
        column: "Name",
        unique: true,
        filter: "[Name] IS NOT NULL");
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropTable(
        name: "Categories");
  }
}

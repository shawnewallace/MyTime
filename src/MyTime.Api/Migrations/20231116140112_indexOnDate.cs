using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTime.Api.Migrations;

/// <inheritdoc />
public partial class indexOnDate : Migration
{
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.CreateIndex(
        name: "IX_Entries_OnDate",
        table: "Entries",
        column: "OnDate");
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropIndex(
        name: "IX_Entries_OnDate",
        table: "Entries");
  }
}

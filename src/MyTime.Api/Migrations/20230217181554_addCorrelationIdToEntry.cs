using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTime.Api.Migrations;

public partial class addCorrelationIdToEntry : Migration
{
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.AddColumn<string>(
        name: "CorrelationId",
        table: "Entries",
        type: "nvarchar(max)",
        nullable: true);
  }

  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropColumn(
        name: "CorrelationId",
        table: "Entries");
  }
}

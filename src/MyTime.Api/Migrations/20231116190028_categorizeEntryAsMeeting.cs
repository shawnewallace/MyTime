using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTime.Api.Migrations;

/// <inheritdoc />
public partial class categorizeEntryAsMeeting : Migration
{
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.AddColumn<bool>(
        name: "IsMeeting",
        table: "Entries",
        type: "bit",
        nullable: false,
        defaultValue: false);
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropColumn(
        name: "IsMeeting",
        table: "Entries");
  }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTime.Api.Migrations;

public partial class addCategoryToEntry : Migration
{
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.AlterColumn<string>(
        name: "Description",
        table: "Entries",
        type: "nvarchar(50)",
        maxLength: 50,
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)",
        oldNullable: true);

    migrationBuilder.AddColumn<string>(
        name: "Category",
        table: "Entries",
        type: "nvarchar(50)",
        maxLength: 50,
        nullable: true);
  }

  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropColumn(
        name: "Category",
        table: "Entries");

    migrationBuilder.AlterColumn<string>(
        name: "Description",
        table: "Entries",
        type: "nvarchar(max)",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(50)",
        oldMaxLength: 50,
        oldNullable: true);
  }
}

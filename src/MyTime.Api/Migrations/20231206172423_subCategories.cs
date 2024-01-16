using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTime.Api.Migrations;

/// <inheritdoc />
public partial class subCategories : Migration
{
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropIndex(
        name: "IX_Categories_Name",
        table: "Categories");

    migrationBuilder.AlterColumn<string>(
        name: "Notes",
        table: "Entries",
        type: "nvarchar(max)",
        nullable: false,
        defaultValue: "",
        oldClrType: typeof(string),
        oldType: "nvarchar(max)",
        oldNullable: true);

    migrationBuilder.AlterColumn<string>(
        name: "Description",
        table: "Entries",
        type: "nvarchar(255)",
        maxLength: 255,
        nullable: false,
        defaultValue: "",
        oldClrType: typeof(string),
        oldType: "nvarchar(255)",
        oldMaxLength: 255,
        oldNullable: true);

    migrationBuilder.AlterColumn<string>(
        name: "CorrelationId",
        table: "Entries",
        type: "nvarchar(max)",
        nullable: false,
        defaultValue: "",
        oldClrType: typeof(string),
        oldType: "nvarchar(max)",
        oldNullable: true);

    migrationBuilder.AlterColumn<string>(
        name: "Category",
        table: "Entries",
        type: "nvarchar(50)",
        maxLength: 50,
        nullable: false,
        defaultValue: "",
        oldClrType: typeof(string),
        oldType: "nvarchar(50)",
        oldMaxLength: 50,
        oldNullable: true);

    migrationBuilder.AlterColumn<string>(
        name: "Name",
        table: "Categories",
        type: "nvarchar(50)",
        maxLength: 50,
        nullable: false,
        defaultValue: "",
        oldClrType: typeof(string),
        oldType: "nvarchar(50)",
        oldMaxLength: 50,
        oldNullable: true);

    migrationBuilder.AddColumn<Guid>(
        name: "ParentId",
        table: "Categories",
        type: "uniqueidentifier",
        nullable: true);

    migrationBuilder.CreateIndex(
        name: "IX_Categories_Name",
        table: "Categories",
        column: "Name",
        unique: true);

    migrationBuilder.CreateIndex(
        name: "IX_Categories_ParentId",
        table: "Categories",
        column: "ParentId");

    migrationBuilder.AddForeignKey(
        name: "FK_Categories_Categories_ParentId",
        table: "Categories",
        column: "ParentId",
        principalTable: "Categories",
        principalColumn: "Id",
        onDelete: ReferentialAction.Restrict);
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropForeignKey(
        name: "FK_Categories_Categories_ParentId",
        table: "Categories");

    migrationBuilder.DropIndex(
        name: "IX_Categories_Name",
        table: "Categories");

    migrationBuilder.DropIndex(
        name: "IX_Categories_ParentId",
        table: "Categories");

    migrationBuilder.DropColumn(
        name: "ParentId",
        table: "Categories");

    migrationBuilder.AlterColumn<string>(
        name: "Notes",
        table: "Entries",
        type: "nvarchar(max)",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)");

    migrationBuilder.AlterColumn<string>(
        name: "Description",
        table: "Entries",
        type: "nvarchar(255)",
        maxLength: 255,
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(255)",
        oldMaxLength: 255);

    migrationBuilder.AlterColumn<string>(
        name: "CorrelationId",
        table: "Entries",
        type: "nvarchar(max)",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(max)");

    migrationBuilder.AlterColumn<string>(
        name: "Category",
        table: "Entries",
        type: "nvarchar(50)",
        maxLength: 50,
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(50)",
        oldMaxLength: 50);

    migrationBuilder.AlterColumn<string>(
        name: "Name",
        table: "Categories",
        type: "nvarchar(50)",
        maxLength: 50,
        nullable: true,
        oldClrType: typeof(string),
        oldType: "nvarchar(50)",
        oldMaxLength: 50);

    migrationBuilder.CreateIndex(
        name: "IX_Categories_Name",
        table: "Categories",
        column: "Name",
        unique: true,
        filter: "[Name] IS NOT NULL");
  }
}

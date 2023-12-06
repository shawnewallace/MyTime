using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTime.Api.Migrations
{
	/// <inheritdoc />
	public partial class entryCategory : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<Guid>(
					name: "CategoryId",
					table: "Entries",
					type: "uniqueidentifier",
					nullable: true);

			migrationBuilder.CreateIndex(
					name: "IX_Entries_CategoryId",
					table: "Entries",
					column: "CategoryId");

			migrationBuilder.AddForeignKey(
					name: "FK_Entries_Categories_CategoryId",
					table: "Entries",
					column: "CategoryId",
					principalTable: "Categories",
					principalColumn: "Id");

			migrationBuilder.Sql(@"update entries
					set CategoryId = c.Id
					from entries e join Categories c on e.Category = c.Name");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
					name: "FK_Entries_Categories_CategoryId",
					table: "Entries");

			migrationBuilder.DropIndex(
					name: "IX_Entries_CategoryId",
					table: "Entries");

			migrationBuilder.DropColumn(
					name: "CategoryId",
					table: "Entries");
		}
	}
}

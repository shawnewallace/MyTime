using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTime.Api.Migrations
{
	public partial class AddingOnDateForEntries : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<DateTime>(
					name: "OnDate",
					table: "Entries",
					type: "date",
					nullable: false,
					defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
					name: "OnDate",
					table: "Entries");
		}
	}
}

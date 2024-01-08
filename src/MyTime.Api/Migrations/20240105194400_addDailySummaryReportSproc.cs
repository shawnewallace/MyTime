using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTime.Api.Migrations
{
	/// <inheritdoc />
	public partial class addDailySummaryReportSproc : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			var sp = @"CREATE PROCEDURE GetDailySummaryReport
						@StartDate DATETIME,
						@EndDate DATETIME
					AS
					BEGIN
						select
							e.OnDate,
							p.Name as parentCategory,
							c.Name as category,
							string_agg(e.Description, char(13)) as description,
							sum(e.Duration) as duratation
						from Entries e
							left join categories c on e.CategoryId = c.Id
							left join categories p on c.ParentId = p.Id
						where e.OnDate between @StartDate and @EndDate and e.IsDeleted = 0
						group by e.OnDate, p.Name, c.Name
						order by e.OnDate desc, p.Name, c.Name;
					END";
			migrationBuilder.Sql(sp);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetDailySummaryReport");
		}
	}
}

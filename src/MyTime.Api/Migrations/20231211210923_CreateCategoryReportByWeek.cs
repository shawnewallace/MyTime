using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTime.Api.Migrations
{
	/// <inheritdoc />
	public partial class CreateCategoryReportByWeek : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			var sp = @"CREATE PROCEDURE GetCategoryReportByWeek
						@StartDate DATETIME,
						@EndDate DATETIME
					AS
					BEGIN
						select
						DATEPART(YEAR, e.OnDate) as Year,
						DATEPART(WEEK, e.OnDate) as Week,
						parent.Name as ParentCategory,
						c.Name as Category,
						STRING_AGG(CONCAT('[', e.Description, ']'), ', ') as Summary,
						sum(duration) as TotalHours
				from Entries e
						left join Categories c on e.CategoryId = c.Id
						left join Categories parent on c.ParentId = parent.Id
				where
						e.IsDeleted = 0 and
						e.OnDate between @StartDate and @EndDate
				group by DATEPART(YEAR, e.OnDate), DATEPART(WEEK, e.OnDate), parent.Name, c.Name
				order by DATEPART(YEAR, e.OnDate) desc, DATEPART(WEEK, e.OnDate) desc, parent.Name, c.Name
					END";
			migrationBuilder.Sql(sp);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetCategoryReportByWeek");
		}
	}
}

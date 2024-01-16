using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTime.Api.Migrations;

/// <inheritdoc />
public partial class CreateSummaryReportByWeek : Migration
{
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    string sp = @"CREATE PROCEDURE GetSummaryReportByWeek
						@StartDate DATETIME,
						@EndDate DATETIME
					AS
					BEGIN
						select
							DATEPART(YEAR, e.OnDate) as Year,
							DATEPART(WEEK, e.OnDate) as Week,
							sum(duration) as TotalHours,
							sum(iif(e.IsUtilization = 1, e.duration, 0.0)) as UtilizedHours,
							sum(IIF(e.isMeeting = 1, e.duration, 0.0)) as MeetingHours,
							sum(iif(parent.Name = 'Business Development', e.Duration, 0.0)) as BusinessDevelopmentHours
					from Entries e
							left join Categories c on e.CategoryId = c.Id
							left join Categories parent on c.ParentId = parent.Id
					where
							e.IsDeleted = 0 and
							e.OnDate between @StartDate and @EndDate
					group by DATEPART(YEAR, e.OnDate), DATEPART(WEEK, e.OnDate)
					order by DATEPART(YEAR, e.OnDate) desc, DATEPART(WEEK, e.OnDate) desc;
					END";
    migrationBuilder.Sql(sp);
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetSummaryReportByWeek");
  }
}

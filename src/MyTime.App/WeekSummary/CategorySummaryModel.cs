using System;
using MyTime.App.Infrastructure;

namespace MyTime.App.WeekSummary;

public sealed record CategorySummaryModel(
	int Year,
	int Week,
	string? ParentCategory,
	string? Category,
	string Summary,
	double TotalHours)
{
	public DateTime FirstDayOfWeek => new WeekOfYear(Year, Week).FirstDayOfWeek();
	public DateTime LastDayOfWeek => new WeekOfYear(Year, Week).LastDayOfWeek();
}

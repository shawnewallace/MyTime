using System;
using Microsoft.Identity.Client;
using MyTime.App.Infrastructure;

namespace MyTime.App.WeekSummary;

public sealed record WeekSummaryModel(
	int Year,
	int WeekNumber,
	double TotalHours,
	double UtilizedHours,
	double MeetingHours,
	double BusinessDevelopmentHours)
{
	public DateTime FirstDayOfWeek => new WeekOfYear(Year, WeekNumber).FirstDayOfWeek();
	public DateTime LastDayOfWeek => new WeekOfYear(Year, WeekNumber).LastDayOfWeek();

	public double UtilizedPercentage
	{
		get
		{
			if (TotalHours == 0f) return 0f;
			return (UtilizedHours / TotalHours) * 100f;
		}
	}

	public double MeetingHoursPercentage
	{
		get
		{
			if (TotalHours == 0f) return 0f;
			return (MeetingHours / TotalHours) * 100f;
		}
	}

	public double BusinessDevelopmentHoursPercentage
	{
		get
		{
			if (TotalHours == 0f) return 0f;
			return (BusinessDevelopmentHours / TotalHours) * 100f;
		}
	}
}



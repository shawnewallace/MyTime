using System;
using MyTime.App.Infrastructure;

namespace MyTime.App.WeekSummary;

public sealed record WeekSummaryModel(
	int Year,
	int WeekNumber,
	DateTime FirstDayOfWeek,
	DateTime LastDayOfWeek,
	float TotalHours,
	float UtilizedHours,
	float MeetingHours,
	float BusinessDevelopmentHours)
{
	public float UtilizedPercentage
	{
		get
		{
			if (TotalHours == 0f) return 0f;
			return UtilizedHours / TotalHours;
		}
	}

	public float MeetingHoursPercentage
	{
		get
		{
			if (TotalHours == 0f) return 0f;
			return MeetingHours / TotalHours;
		}
	}

	public float BusinessDevelopmentHoursPercentage
	{
		get
		{
			if (TotalHours == 0f) return 0f;
			return BusinessDevelopmentHours / TotalHours;
		}
	}
}



using System;
using Microsoft.Identity.Client;

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
			return (UtilizedHours / TotalHours) * 100f;
		}
	}

	public float MeetingHoursPercentage
	{
		get
		{
			if (TotalHours == 0f) return 0f;
			return (MeetingHours / TotalHours) * 100f;
		}
	}

	public float BusinessDevelopmentHoursPercentage
	{
		get
		{
			if (TotalHours == 0f) return 0f;
			return (BusinessDevelopmentHours / TotalHours) * 100f;
		}
	}
}



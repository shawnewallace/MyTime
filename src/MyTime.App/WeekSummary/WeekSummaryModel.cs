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
	float BusinessDevelopmentHours);


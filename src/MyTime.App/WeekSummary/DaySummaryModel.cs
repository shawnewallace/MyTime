using System;

namespace MyTime.App.WeekSummary;

public sealed record DaySummaryModel(
	DateTime OnDate, 
	string ParentCategory, 
	string Category, 
	double Duration);



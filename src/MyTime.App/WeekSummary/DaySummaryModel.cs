using System;

namespace MyTime.App.WeekSummary;

public sealed record DaySummaryModel(
	DateTime OnDate, 
	string ParentCategory, 
	string Category, 
	string Description,
	double Duration)
	{
	public string FullName => string.IsNullOrEmpty(ParentCategory) ? Category : $"{ParentCategory}:{Category}";
};



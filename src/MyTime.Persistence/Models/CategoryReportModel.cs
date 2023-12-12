using System;

namespace MyTime.Persistence.Models;

public sealed class CategoryReportModel
{
	public int Year { get; set; }
	public int Week { get; set; }
	public string? ParentCategory { get; set; } = string.Empty;
	public string? Category { get; set; } = string.Empty;
	public string Summary { get; set; } = string.Empty;
	public double TotalHours { get; set; }
}

using System;
using System.Globalization;

namespace MyTime.App.Infrastructure;

public static class DateTimeExtensions
{
	public static DateTime FirstDayOfWeek(this DateTime dt)
	{
		var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
		var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;

		if (diff < 0)
		{
			diff += 7;
		}

		return dt.AddDays(-diff).Date;
	}

	public static DateTime FirstDayOfWeek(this WeekOfYear week)
	{
		var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
		var firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;
		var firstDayOfYear = new DateTime(week.Year, 1, 1);
		var firstDayOfFirstWeek = firstDayOfYear.FirstDayOfWeek();

		var daysToAdd = (week.Week - 1) * 7;

		return firstDayOfFirstWeek.AddDays(daysToAdd);
	}

	public static DateTime LastDayOfWeek(this DateTime dt) => dt.FirstDayOfWeek().AddDays(6);
	public static DateTime LastDayOfWeek(this WeekOfYear week) => week.FirstDayOfWeek().AddDays(6);
	public static DateTime FirstDayOfMonth(this DateTime dt) => new DateTime(dt.Year, dt.Month, 1);
	public static DateTime LastDayOfMonth(this DateTime dt) => dt.FirstDayOfMonth().AddMonths(1).AddDays(-1);
	public static DateTime FirstDayOfNextMonth(this DateTime dt) => dt.FirstDayOfMonth().AddMonths(1);

	public static int WeekNumber(this DateTime dt)
	{
		var cultureInfo = CultureInfo.CurrentCulture;

		return cultureInfo
			.Calendar
			.GetWeekOfYear(
				dt,
				cultureInfo.DateTimeFormat.CalendarWeekRule,
				cultureInfo.DateTimeFormat.FirstDayOfWeek);
	}
}

public record WeekOfYear(int Year, int Week);

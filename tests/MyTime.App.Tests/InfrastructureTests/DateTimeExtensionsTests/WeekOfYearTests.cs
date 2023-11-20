using System;
using Shouldly;
using Xunit;
using MyTime.App.Infrastructure;

namespace MyTime.App.Tests.InfrastructureTests.DateTimeExtensionsTests;

public class WeekOfYearTests
{
	public static readonly object[][] FirstDayOfWeekData = {
		new object[] { new WeekOfYear(2022, 01), new DateTime(2021, 12, 26) },
		new object[] { new WeekOfYear(2023, 01), new DateTime(2023, 01, 01) },
		new object[] { new WeekOfYear(2023, 15), new DateTime(2023, 04, 09) },
		new object[] { new WeekOfYear(2023, 53), new DateTime(2023, 12, 31) },
		new object[] { new WeekOfYear(2023, 24), new DateTime(2023, 06, 11) },
		new object[] { new WeekOfYear(2023, 52), new DateTime(2023, 12, 24) }
	};

	[Theory, MemberData(nameof(FirstDayOfWeekData))] public void CalculatesFirstDayOfWeek(WeekOfYear week, DateTime expectedFirstDayOfWeek) => week.FirstDayOfWeek().ShouldBe(expectedFirstDayOfWeek);

	public static readonly object[][] LastDayOfWeekData = {
		new object[] { new WeekOfYear(2022, 01), new DateTime(2022, 01, 01) },
		new object[] { new WeekOfYear(2023, 01), new DateTime(2023, 01, 07) },
		new object[] { new WeekOfYear(2023, 15), new DateTime(2023, 04, 15) },
		new object[] { new WeekOfYear(2023, 53), new DateTime(2024, 01, 06) },
		new object[] { new WeekOfYear(2023, 24), new DateTime(2023, 06, 17) },
		new object[] { new WeekOfYear(2023, 52), new DateTime(2023, 12, 30) }
	};

	[Theory, MemberData(nameof(LastDayOfWeekData))] public void CalculatesLastDayOfWeek(WeekOfYear week, DateTime expectedLastDayOfWeek) => week.LastDayOfWeek().ShouldBe(expectedLastDayOfWeek);
}
using System;
using Shouldly;
using Xunit;
using MyTime.App.Infrastructure;

namespace MyTime.App.Tests.InfrastructureTests.DateTimeExtensionsTests
{
	public class FirstDayOfMonthTests
	{
		private DateTime date = new DateTime(2022, 6, 16);

		[Fact] public void TheDayIsOne() => date.FirstDayOfMonth().Day.ShouldBe(1);
		[Fact] public void TheMonthIsSame() => date.FirstDayOfMonth().Month.ShouldBe(date.Month);
		[Fact] public void TheYearIsSame() => date.FirstDayOfMonth().Year.ShouldBe(date.Year);
	}
}
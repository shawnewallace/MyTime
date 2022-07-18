using System;
using Shouldly;
using Xunit;
using MyTime.App.Infrastructure;

namespace MyTime.App.Tests.InfrastructureTests.DateTimeExtensionsTests
{
	public class LastDayOfMonthTests
	{
		[Fact] public void JulyIs31() => new DateTime(2022, 7, 14).LastDayOfMonth().ShouldBe(new DateTime(2022, 7, 31));
		[Fact] public void SeptemberIs30() => new DateTime(2017, 9, 7).LastDayOfMonth().ShouldBe(new DateTime(2017, 9, 30));
		[Fact] public void FebruaryIs28() => new DateTime(2022, 2, 11).LastDayOfMonth().ShouldBe(new DateTime(2022, 2, 28));
		[Fact] public void ExceptForLeapYear() => new DateTime(2020, 2, 11).LastDayOfMonth().ShouldBe(new DateTime(2020, 2, 29));
	}
}
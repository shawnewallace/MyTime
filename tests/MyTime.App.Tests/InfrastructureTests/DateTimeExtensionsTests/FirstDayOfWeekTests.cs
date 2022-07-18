using System;
using Shouldly;
using Xunit;
using MyTime.App.Infrastructure;

namespace MyTime.App.Tests.InfrastructureTests.DateTimeExtensionsTests
{
	public class FirstDayOfWeekTests
	{
		private DateTime sunday = new DateTime(2022, 07, 31);

		[Fact] public void Sunday() => sunday.FirstDayOfWeek().ShouldBe(sunday);
		[Fact] public void Monday() => new DateTime(2022, 08, 1).FirstDayOfWeek().ShouldBe(sunday);
		[Fact] public void Tuesday() => new DateTime(2022, 08, 2).FirstDayOfWeek().ShouldBe(sunday);
		[Fact] public void Wednesday() => new DateTime(2022, 08, 3).FirstDayOfWeek().ShouldBe(sunday);
		[Fact] public void Thursday() => new DateTime(2022, 08, 4).FirstDayOfWeek().ShouldBe(sunday);
		[Fact] public void Friday() => new DateTime(2022, 08, 5).FirstDayOfWeek().ShouldBe(sunday);
		[Fact] public void Saturday() => new DateTime(2022, 08, 6).FirstDayOfWeek().ShouldBe(sunday);
	}
}
using System;
using MyTime.App.Infrastructure;
using Shouldly;
using Xunit;

namespace MyTime.App.Tests.InfrastructureTests.DateTimeExtensionsTests;

public class FirstDayOfNextMonthTests
{
  private readonly DateTime date = new DateTime(2022, 6, 16);

  [Fact] public void TheDayIsOne() => date.FirstDayOfNextMonth().Day.ShouldBe(1);
  [Fact] public void TheMonthIsPlusOne() => date.FirstDayOfNextMonth().Month.ShouldBe(date.Month + 1);
  [Fact] public void TheYearIsSame() => date.FirstDayOfNextMonth().Year.ShouldBe(date.Year);
  [Fact] public void ItWorksForDecember() => new DateTime(2019, 12, 25).FirstDayOfNextMonth().Year.ShouldBe(2020);
  [Fact] public void ItWorksForDecemberMonth() => new DateTime(2019, 12, 25).FirstDayOfNextMonth().Month.ShouldBe(1);
}

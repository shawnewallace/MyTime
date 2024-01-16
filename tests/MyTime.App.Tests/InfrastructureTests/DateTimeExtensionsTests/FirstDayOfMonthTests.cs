using System;
using MyTime.App.Infrastructure;
using Shouldly;
using Xunit;

namespace MyTime.App.Tests.InfrastructureTests.DateTimeExtensionsTests;

public class FirstDayOfMonthTests
{
  private readonly DateTime date = new DateTime(2022, 6, 16);

  [Fact] public void TheDayIsOne() => date.FirstDayOfMonth().Day.ShouldBe(1);
  [Fact] public void TheMonthIsSame() => date.FirstDayOfMonth().Month.ShouldBe(date.Month);
  [Fact] public void TheYearIsSame() => date.FirstDayOfMonth().Year.ShouldBe(date.Year);
}

using System;
using MyTime.App.Infrastructure;
using Shouldly;
using Xunit;

namespace MyTime.App.Tests.InfrastructureTests.DateTimeExtensionsTests;

public class LastDayOfWeekTests
{
  private readonly DateTime saturday = new DateTime(2022, 08, 06);

  [Fact] public void Sunday() => new DateTime(2022, 07, 31).LastDayOfWeek().ShouldBe(saturday);
  [Fact] public void Monday() => new DateTime(2022, 08, 1).LastDayOfWeek().ShouldBe(saturday);
  [Fact] public void Tuesday() => new DateTime(2022, 08, 2).LastDayOfWeek().ShouldBe(saturday);
  [Fact] public void Wednesday() => new DateTime(2022, 08, 3).LastDayOfWeek().ShouldBe(saturday);
  [Fact] public void Thursday() => new DateTime(2022, 08, 4).LastDayOfWeek().ShouldBe(saturday);
  [Fact] public void Friday() => new DateTime(2022, 08, 5).LastDayOfWeek().ShouldBe(saturday);
  [Fact] public void Saturday() => new DateTime(2022, 08, 6).LastDayOfWeek().ShouldBe(saturday);
}

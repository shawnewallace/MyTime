using System;
using MyTime.App.Infrastructure;
using Shouldly;
using Xunit;

namespace MyTime.App.Tests.InfrastructureTests.DateTimeExtensionsTests;

public class WeekNumberTests
{
  public static readonly object[][] TestData = {
    new object[] { new DateTime(2023, 01, 01), 1 },
    new object[] { new DateTime(2022, 01, 01), 1 },
    new object[] { new DateTime(2023, 4, 15), 15 },
    new object[] { new DateTime(2023, 12, 31), 53 },
    new object[] { new DateTime(2023, 06, 16), 24 },
    new object[] { new DateTime(2023, 12, 25), 52 },
    new object[] { new DateTime(2023, 07, 04), 27 },
    new object[] { new DateTime(2023, 10, 22), 43 },
    new object[] { new DateTime(2023, 11, 10), 45 },
    new object[] { new DateTime(2023, 11, 11), 45 },
  };

  [Theory, MemberData(nameof(TestData))] public void IsRightWeek(DateTime dt, int weekOfYear) => dt.WeekNumber().ShouldBe(weekOfYear);
}

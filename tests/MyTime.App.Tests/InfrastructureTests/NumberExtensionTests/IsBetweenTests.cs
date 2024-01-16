using System;
using MyTime.App.Infrastructure;
using Shouldly;
using Xunit;

namespace MyTime.App.Tests.InfrastructureTests.NumberExtensionsTests;

public class IsBetweenTests
{
  [Theory]
  [InlineData(5, 4, 6, false, true)]
  [InlineData(5, 5, 6, false, false)]
  [InlineData(5, 4, 5, false, false)]
  [InlineData(5, 5, 6, true, true)]
  [InlineData(5, 4, 5, true, true)]
  [InlineData(3, 4, 5, true, false)]
  [InlineData(7, 4, 5, true, false)]
  public void ShouldWorkCorrectly(float value, float lower, float upper, bool inclusive, bool expected)
  {
    bool result = value.IsBetween(lower, upper, inclusive);

    result.ShouldBe(expected);
  }
}

using Xunit;
using Shouldly;
using MyTime.App.Infrastructure;

namespace MyTime.App.Tests.InfrastructureTests.NumberExtensionsTests;

public class RoundToQuarterTests
{
	[Theory]
	[InlineData(1.11f, 1.0f)]
	[InlineData(1.12f, 1.0f)]
	[InlineData(1.13f, 1.25f)]
	[InlineData(1.37f, 1.25f)]
	[InlineData(1.38f, 1.5f)]
	[InlineData(1.62f, 1.5f)]
	[InlineData(1.63f, 1.75f)]
	[InlineData(1.87f, 1.75f)]
	[InlineData(1.88f, 2.0f)]
	[InlineData(0.0f, 0.0f)]
	public void ShouldRoundCorrectly(float value, float expected)
	{
		float result = value.RoundToQuarter();

		result.ShouldBe(expected);
	}
}

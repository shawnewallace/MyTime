using System;

namespace MyTime.App.Infrastructure;

public static class NumberExtensions
{
	public static bool IsBetween(this float value,
		float lower,
		float upper,
		bool inclusive = false)
	{
		if (inclusive) return value >= lower && value <= upper;

		return value > lower && value < upper;
	}

	public static float RoundToQuarter(this float value) => (float)Math.Round(value * 4, MidpointRounding.ToEven) / 4;
}
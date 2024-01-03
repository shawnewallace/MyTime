using System;
using MyTime.App.WeekSummary;
using Shouldly;
using Xunit;

namespace MyTime.App.Tests.ModelsTests;

public class WeekSummaryModelTests
{
	[Theory]
	[InlineData(0f, 2f, 0f)]
	[InlineData(10f, 5f, 50.0f)]
	[InlineData(40.75f, 10.25f, 25.153374f)]
	[InlineData(40.75f, 0f, 0)]
	public void ItCalculatesUtilizedPercentageProperly(float totalHours, float utilizedHOurs, float expected) {
		var model = new WeekSummaryModel(2023, 10, totalHours, utilizedHOurs, 0f, 0f);
		model.UtilizedPercentage.ShouldBe(expected, tolerance: 0.0001f);
	}

	[Theory]
	[InlineData(0f, 2f, 0f)]
	[InlineData(10f, 5f, 50.0f)]
	[InlineData(40.75f, 10.25f, 25.153374f)]
	[InlineData(40.75f, 0f, 0)]
	public void ItCalculatesMeetingHoursProperly(float totalHours, float meetingHours, float expected) {
		var model = new WeekSummaryModel(2023, 10, totalHours, 0f, meetingHours, 0f);
		model.MeetingHoursPercentage.ShouldBe(expected, tolerance: 0.0001f);
	}

	[Theory]
	[InlineData(0f, 2f, 0f)]
	[InlineData(10f, 5f, 50.0f)]
	[InlineData(40.75f, 10.25f, 25.153374f)]
	[InlineData(40.75f, 0f, 0)]
	public void ItCalculatesBusinessDevelopmentHoursProperly(float totalHours, float businessDevelopmentHours, float expected) {
		var model = new WeekSummaryModel(2023, 10, totalHours, 0f, 0f, businessDevelopmentHours);
		model.BusinessDevelopmentHoursPercentage.ShouldBe(expected, tolerance: 0.0001f);
	}
}

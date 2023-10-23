using System;
using System.Collections.Generic;
using System.Linq;
using MyTime.App.EntryDays;
using MyTime.Persistence.Entities;
using Shouldly;
using Xunit;

namespace MyTime.App.Tests.ModelsTests;

public class EntryRangeModelTests
{
	private readonly DateTime _rangeStart;
	private readonly DateTime _rangeEnd;
	private readonly List<Entry> _entries;
	private readonly EntryRangeModel _modelUnderTest;

	public EntryRangeModelTests()
	{
		_rangeStart = new DateTime(2023, 10, 01);
		_rangeEnd = new DateTime(2023, 10, 31);
		_entries = new List<Entry>
		{
			new() {
				Id = default,
				WhenCreated = default,
				WhenUpdated = default,
				IsDeleted = false,
				OnDate = new DateTime(2023, 10, 4),
				Description = null,
				Duration = 4f,
				IsUtilization = true,
				Category = "Cat 1",
				Notes = null,
				CorrelationId = null,
				UserId = null
			},
			new() {
				Id = default,
				WhenCreated = default,
				WhenUpdated = default,
				IsDeleted = false,
				OnDate = new DateTime(2023, 10, 10),
				Description = null,
				Duration = 6.25f,
				IsUtilization = false,
				Category = "Cat 2",
				Notes = null,
				CorrelationId = null,
				UserId = null
			},
			new() {
				Id = default,
				WhenCreated = default,
				WhenUpdated = default,
				IsDeleted = false,
				OnDate = new DateTime(2023, 10, 16),
				Description = null,
				Duration = 9.7f,
				IsUtilization = false,
				Category = "Cat 2",
				Notes = null,
				CorrelationId = null,
				UserId = null
			},
			new() {
				Id = default,
				WhenCreated = default,
				WhenUpdated = default,
				IsDeleted = false,
				OnDate = new DateTime(2023, 10, 20),
				Description = null,
				Duration = 1.25f,
				IsUtilization = false,
				Category = "Cat 1",
				Notes = null,
				CorrelationId = null,
				UserId = null
			},
			new()
			{
				Id = default,
				WhenCreated = default,
				WhenUpdated = default,
				IsDeleted = false,
				OnDate = new DateTime(2023, 10, 6),
				Description = null,
				Duration = .25f,
				IsUtilization = true,
				Category = null,
				Notes = null,
				CorrelationId = null,
				UserId = null
			}
		};

		_modelUnderTest = new EntryRangeModel(_rangeStart, _rangeEnd, _entries);
	}

	[Fact]
	public void NumEntriesShouldBe5() =>
		_modelUnderTest.NumEntries.ShouldBe(_entries.Count);

	[Fact]
	public void RangeStartShouldBeInitialized() =>	
		_modelUnderTest.RangeStart.ShouldBe(_rangeStart);

	[Fact]
	public void RangeEndShouldBeInitialized() =>
		_modelUnderTest.RangeEnd.ShouldBe(_rangeEnd);

	[Fact]
	public void TotalShouldBeCalculatedCorrectly() => 
		_modelUnderTest.Total.ShouldBe(_entries.Sum(e => e.Duration));

	[Fact]
	public void UtilizedTotalShouldBeCalculatedCorrectly() => 
		_modelUnderTest.UtilizedTotal.ShouldBe(_entries.Where(e => e.IsUtilization).Sum(e => e.Duration));

	[Fact]public void NumberOfCategorySummariesShouldBeThree() => 
		_modelUnderTest.NumCategories.ShouldBe(3);

	[Theory]
	[InlineData(null, .25f, 1, "10/06/2023", "10/06/2023")]
	[InlineData("Cat 1", 5.25f, 2, "10/04/2023", "10/20/2023")]
	[InlineData("Cat 2", 15.95f, 2, "10/10/2023", "10/16/2023")]
	public void CategoryValuesShouldBeCorrect(string category,
		float totalDuration,
		int numEntries,
		string firstEntry,
		string lastEntry)
	{
		var cat = _modelUnderTest.Categories.First(c => c.Name == category);

		cat.Total.ShouldBe(totalDuration);
		cat.NumEntries.ShouldBe(numEntries);
		cat.FirstEntry.ShouldBe(DateTime.Parse(firstEntry));
		cat.LastEntry.ShouldBe(DateTime.Parse(lastEntry));
	}
}
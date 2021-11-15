using Shouldly;
using System;
using Xunit;

namespace MyTime.App.Models.Tests
{
	public class EntryDayModelTests
	{
		private readonly EntryDay _day;

		public EntryDayModelTests() => _day = new EntryDay(2021, 5, 14);

		[Fact] public void DayOfMonthShouldBe14() => _day.DayOfMonth.ShouldBe(14);
		[Fact] public void EntriesIsEmptyByDefault() => _day.Entries.ShouldBeEmpty();
		[Fact] public void EntriesIsInstantiatedByDefault() => _day.Entries.ShouldNotBeNull();
		[Fact] public void MonthShouldBe5() => _day.Month.ShouldBe(5);
		[Fact] public void TotalIsZeroWithoutEntries() => _day.Total().ShouldBe(0.0F);
		[Fact] public void YearShouldBe202() => _day.Year.ShouldBe(2021);

		[Fact]
		public void TotalSumsAllDurations()
		{
			AddSampleEntries();
			_day.Total().ShouldBe(48.3F);
		}

		[Fact] public void UtilizedTotalIsZeroWithoutEntries() => _day.UtilizedTotal().ShouldBe(0.0F);

		[Fact]
		public void UtilizedTotalSumsAllDurations()
		{
			AddSampleEntries();
			_day.UtilizedTotal().ShouldBe(25.3F);
		}
		private void AddSampleEntries()
		{
			_day.Entries.Add(new Entry { Duration = 10.0F, IsUtilization = true });
			_day.Entries.Add(new Entry { Duration = 15.3F, IsUtilization = true });
			_day.Entries.Add(new Entry { Duration = 11.7F, IsUtilization = false });
			_day.Entries.Add(new Entry { Duration = 4.0F, IsUtilization = false });
			_day.Entries.Add(new Entry { Duration = 7.3F, IsUtilization = false });
		}
	}
}

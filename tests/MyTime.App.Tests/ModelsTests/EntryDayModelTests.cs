using MyTime.App.Models;
using Shouldly;
using Xunit;

namespace MyTime.App.Tests.ModelTests
{
	public class EntryDayModelTests
	{
		private readonly EntryDayModel _day;

		public EntryDayModelTests() => _day = new EntryDayModel(2021, 5, 14);

		[Fact] public void DayOfMonthShouldBe14() => _day.DayOfMonth.ShouldBe(14);
		[Fact] public void EntriesIsEmptyByDefault() => _day.Entries.ShouldBeEmpty();
		[Fact] public void EntriesIsInstantiatedByDefault() => _day.Entries.ShouldNotBeNull();
		[Fact] public void MonthShouldBe5() => _day.Month.ShouldBe(5);
		[Fact] public void YearShouldBe202() => _day.Year.ShouldBe(2021);

		[Fact] public void TotalIsZeroWithoutEntries() => _day.Total.ShouldBe(0.0F);

		[Fact]
		public void TotalSumsAllDurations()
		{
			AddSampleEntries();
			_day.Total.ShouldBe(48.3F);
		}

		[Fact] public void UtilizedTotalIsZeroWithoutEntries() => _day.UtilizedTotal.ShouldBe(0.0F);

		[Fact]
		public void UtilizedTotalSumsAllDurations()
		{
			AddSampleEntries();
			_day.UtilizedTotal.ShouldBe(25.3F);
		}
		private void AddSampleEntries()
		{
			_day.Entries.Add(new EntryModel { Duration = 10.0F, IsUtilization = true });
			_day.Entries.Add(new EntryModel { Duration = 15.3F, IsUtilization = true });
			_day.Entries.Add(new EntryModel { Duration = 11.7F, IsUtilization = false });
			_day.Entries.Add(new EntryModel { Duration = 4.0F, IsUtilization = false });
			_day.Entries.Add(new EntryModel { Duration = 7.3F, IsUtilization = false });
		}
	}
}

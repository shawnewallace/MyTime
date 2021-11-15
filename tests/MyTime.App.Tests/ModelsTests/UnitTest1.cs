using Shouldly;
using System;
using Xunit;

namespace MyTime.App.Models.Tests
{
	public class DayModelTests
	{
		private readonly Day _day;

		public DayModelTests()
		{
			_day = new Day();
		}

		private void AddSampleEntries()
		{
			_day.Entries.Add(new Entry { Duration = 10.0F, IsUtilization = true });
			_day.Entries.Add(new Entry { Duration = 15.3F, IsUtilization = true });
			_day.Entries.Add(new Entry { Duration = 11.7F, IsUtilization = false });
			_day.Entries.Add(new Entry { Duration = 4.0F, IsUtilization = false });
			_day.Entries.Add(new Entry { Duration = 7.3F, IsUtilization = false });
		}

		[Fact]
		public void TotalIsZeroWithoutEntries()
		{
			_day.Total().ShouldBe(0.0F);
		}

		[Fact]
		public void TotalSumsAllDurations()
		{
			AddSampleEntries();
			_day.Total().ShouldBe(48.3F);
		}

		[Fact]
		public void UtilizedTotalIsZeroWithoutEntries()
		{
			_day.UtilizedTotal().ShouldBe(0.0F);
		}

		[Fact]
		public void UtilizedTotalSumsAllDurations()
		{
			AddSampleEntries();
			_day.UtilizedTotal().ShouldBe(25.3F);
		}
	}
}

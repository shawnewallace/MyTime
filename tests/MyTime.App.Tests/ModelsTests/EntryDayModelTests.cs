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
		[Fact] public void MonthShouldBe5() => _day.Month.ShouldBe(5);
		[Fact] public void YearShouldBe202() => _day.Year.ShouldBe(2021);
		[Fact] public void TotalIsZeroWithoutEntries() => _day.Total.ShouldBe(0.0F);
		[Fact] public void UtilizedTotalIsZeroWithoutEntries() => _day.UtilizedTotal.ShouldBe(0.0F);
	}
}

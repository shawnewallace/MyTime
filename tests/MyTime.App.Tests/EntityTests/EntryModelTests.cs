using MyTime.Persistence.Entities;
using Shouldly;
using Xunit;

namespace MyTime.App.Tests.EntityTests
{
	public class EntryModelTests
	{
		private readonly Entry _entry;

		public EntryModelTests() => _entry = new Entry();

		[Fact] public void DefaultDurationIsZero() => _entry.Duration.ShouldBe(0.0F);
		[Fact] public void DefaultIsUtilizationIsTrue() => _entry.IsUtilization.ShouldBeTrue();
	}
}

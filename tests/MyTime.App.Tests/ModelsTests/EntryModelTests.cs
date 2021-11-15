using Shouldly;
using Xunit;

namespace MyTime.App.Models.Tests
{
	public class EntryModelTests
	{
		private readonly Entry _entry;

		public EntryModelTests() => _entry = new Entry();

		[Fact] public void DefaultDurationIsZero() => _entry.Duration.ShouldBe(0.0F);
		[Fact] public void DefaultIsUtilizationIsTrue() => _entry.IsUtilization.ShouldBeTrue();
	}
}

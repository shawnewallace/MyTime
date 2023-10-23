using MyTime.App.Infrastructure;

namespace MyTime.App.EntryDays
{

	public class EntryDayModel : IEntryRollup
	{
		public int Year { get; }
		public int Month { get; }
		public int DayOfMonth { get; }

		public EntryDayModel() { }
		public EntryDayModel(int year, int month, int day)
		{
			Year = year;
			Month = month;
			DayOfMonth = day;
		}

		public float Total { get; set; }
		public float UtilizedTotal { get; set; }
		public int NumEntries { get; internal set; }
	}
}

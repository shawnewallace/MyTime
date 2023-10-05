using System;
using System.Collections.Generic;
using System.Linq;
using MyTime.App.Infrastructure;
using MyTime.Persistence.Entities;

namespace MyTime.App.Models
{
	public class EntryRangeModel : IEntryRollup
	{
		public DateTime RangeStart { get; set; }
		public DateTime RangeEnd { get; set; }

		public float Total { get { return Days.Sum(m => m.Total); } }
		public float UtilizedTotal { get { return Days.Sum(m => m.UtilizedTotal); } }
		public int NumEntries { get; private set; }
		public List<EntryModel> Entries { get; private set; } = new();
		public List<EntryDayModel> Days { get; private set; } = new();
		public EntryRangeModel() { }
		public EntryRangeModel(DateTime from, DateTime to, List<Entry> entries)
		{
			RangeStart = from;
			RangeEnd = to;
			NumEntries = entries.Count();

			FillDays(entries);
			FillEntries(entries);
		}

		private void FillEntries(List<Entry> entries)
		{
			foreach (var entry in entries.OrderBy(m => m.OnDate).ThenBy(m => m.WhenCreated))
			{
				Entries.Add(new EntryModel(entry));
			}
		}

		private void FillDays(List<Entry> entries)
		{
			DateTime iterator = RangeStart;

			while (iterator <= RangeEnd)
			{
				var newDay = new EntryDayModel(iterator.Year, iterator.Month, iterator.Day);
				var entriesForDay = entries.Where(m => m.OnDate.Year == newDay.Year && m.OnDate.Month == newDay.Month && m.OnDate.Day == newDay.DayOfMonth);

				newDay.Total = entriesForDay.Sum(m => m.Duration);
				newDay.UtilizedTotal = entriesForDay.Where(m => m.IsUtilization).Sum(m => m.Duration);
				newDay.NumEntries = entriesForDay.Count();

				Days.Add(newDay);

				iterator = iterator.AddDays(1);
			}
		}
	}
}

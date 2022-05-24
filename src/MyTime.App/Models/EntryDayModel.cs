using System;
using System.Collections.Generic;
using System.Linq;
using MyTime.App.Infrastructure;
using MyTime.Persistence.Entities;

namespace MyTime.App.Models
{
	public class EntryRangeModel : IEntryRollup
	{

		public EntryRangeModel(){}
		public EntryRangeModel(DateTime from, DateTime to)
		{
			RangeStart = from;
			RangeEnd = to;
		}

		public DateTime RangeStart { get; set; }
		public DateTime RangeEnd { get; set; }
		public List<EntryDayModel> Entries { get; set; } = new List<EntryDayModel>();
		public float Total { get { return Entries.Sum(m => m.Total); } }
		public float UtilizedTotal { get { return Entries.Sum(m => m.UtilizedTotal); } }

		public void Add(EntryDayModel day) {
			Entries.Add(day);
		}
	}

	public class EntryDayModel : IEntryRollup
	{
		public List<EntryModel> Entries { get; set; } = new List<EntryModel>();
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

		public float Total { get { return Entries.Sum(m => m.Duration); } }
		public float UtilizedTotal { get { return Entries.Where(m => m.IsUtilization).Sum(m => m.Duration); } }
	}
}

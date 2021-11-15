using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTime.App.Models
{
	public class EntryDay
	{
		public List<Entry> Entries { get; set; } = new List<Entry>();
		public int Year { get; }
		public int Month { get; }
		public int DayOfMonth { get; }

		public EntryDay() { }
		public EntryDay(int year, int month, int day)
		{
			Year = year;
			Month = month;
			DayOfMonth = day;
		}

		public float Total() => Entries.Sum(m => m.Duration);
		public float UtilizedTotal() => Entries.Where(m => m.IsUtilization).Sum(m => m.Duration);
	}
}

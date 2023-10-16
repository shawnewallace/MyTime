using System;
using Microsoft.Identity.Client;
using MyTime.App.Infrastructure;

namespace MyTime.App.Models
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

	public class CategoryDayModel
	{
		public string Name { get; set; }
		public float Total { get; set; }
		public int NumEntries { get; set; }
		public DateTime FirstEntry { get; set; }
		public DateTime LastEntry { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTime.App.Models
{
	public class Day
	{
		public List<Entry> Entries { get; set; } = new List<Entry>();

		public float Total() => Entries.Sum(m => m.Duration);
		public float UtilizedTotal() => Entries.Where(m => m.IsUtilization).Sum(m => m.Duration);
	}

	public class Entry
	{
		public string Description { get; set; }
		public float Duration { get; set; } = 0.0F;
		public bool IsUtilization { get; set; } = true;
		public string Notes { get; set; }
	}
}

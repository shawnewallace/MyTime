using System;
using MyTime.Persistence.Infrastructure;

namespace MyTime.Persistence.Entities
{
	public class Entry : AppEntityBase, IEntry
	{
		public DateTime OnDate { get; set; }
		public string Description { get; set; }
		public float Duration { get; set; } = 0.0F;
		public bool IsUtilization { get; set; } = true;
		public string Notes { get; set; }
	}
}

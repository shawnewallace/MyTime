using System;
using System.ComponentModel.DataAnnotations;
using MyTime.Persistence.Infrastructure;

namespace MyTime.Persistence.Entities
{
	public class Entry : AppEntityBase, IEntry
	{
		public DateTime OnDate { get; set; }
		[StringLength(50)] public string Description { get; set; }
		public float Duration { get; set; } = 0.0F;
		public bool IsUtilization { get; set; } = true;
		[StringLength(50)] public string Category { get; set; }
		public string Notes { get; set; }
	}
}

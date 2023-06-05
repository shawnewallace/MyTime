using System;
using System.ComponentModel.DataAnnotations;
using MyTime.Persistence.Infrastructure;

namespace MyTime.Persistence.Entities
{
	public class Entry : AppEntityBase, IEntry
	{
		public DateTime OnDate { get; set; }
		[StringLength(255)] public string Description { get; set; }
		public float Duration { get; set; } = 0.0F;
		public bool IsUtilization { get; set; } = true;
		[StringLength(50)] public string Category { get; set; }
		public string Notes { get; set; }
		public string CorrelationId { get; set; } = string.Empty;
		[Required] public string UserId { get; set; } = string.Empty;
	}
}

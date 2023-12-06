using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MyTime.Persistence.Infrastructure;

namespace MyTime.Persistence.Entities;

public class Entry : AppEntityBase, IEntry
{
	public DateTime OnDate { get; set; }
	[StringLength(255)] public string Description { get; set; } = string.Empty;
	public float Duration { get; set; }
	public bool IsUtilization { get; set; } = true;
	[StringLength(50)] public string Category { get; set; } = string.Empty;
	public string Notes { get; set; } = string.Empty;
	public string CorrelationId { get; set; } = string.Empty;
	[Required] public string UserId { get; set; } = string.Empty;
	public bool IsMeeting { get; set; }
}
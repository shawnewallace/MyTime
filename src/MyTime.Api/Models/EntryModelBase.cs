using MyTime.Persistence.Infrastructure;

namespace MyTime.Api.Models;

public abstract class EntryModelBase : IEntry
{
	public DateTime OnDate { get; set; }
	public string Description { get; set; } = string.Empty;
	public string Category { get; set; } = string.Empty;
	public float Duration { get; set; }
	public bool IsUtilization { get; set; }
	public string Notes { get; set; } = string.Empty;
}
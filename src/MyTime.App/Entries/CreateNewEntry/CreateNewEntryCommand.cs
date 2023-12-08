using System;
using MediatR;

namespace MyTime.App.Entries.CreateNewEntry;

public class CreateNewEntryCommand : IRequest<EntryModel>
{
	public DateTime OnDate { get; set; }
	public string Description { get; set; } = string.Empty;
	public float Duration { get; set; }
	public bool IsUtilization { get; set; }
	public Guid? CategoryId { get; set; }
	public string Notes { get; set; } = string.Empty;
	public string CorrelationId { get; set; } = String.Empty;
	public string UserId { get; set; } = string.Empty;
	public bool IsMeeting { get; set; } = false;
}
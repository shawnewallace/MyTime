using System;
using MyTime.Persistence.Entities;
using MyTime.Persistence.Infrastructure;

namespace MyTime.App.Entries;

public class EntryModel : IId<Guid>, ITracking
{
	public DateTime OnDate { get; set; }
	public string Description { get; set; }
	public float Duration { get; set; }
	public bool IsUtilization { get; set; }
	public string Notes { get; set; }
	public Guid Id { get; set; }
	public DateTime WhenCreated { get; set; }
	public DateTime WhenUpdated { get; set; }
	public bool IsDeleted { get; set; }
	public string CorrelationId { get; set; }
	public string Category { get; set; }
	public string UserId { get; set; }
	public bool IsMeeting { get; set; }

	public EntryModel() { }
	public EntryModel(Entry entry)
	{
		Id = entry.Id;
		OnDate = entry.OnDate;
		Description = entry.Description;
		Duration = entry.Duration;
		IsUtilization = entry.IsUtilization;
		Notes = entry.Notes;
		WhenCreated = entry.WhenCreated;
		WhenUpdated = entry.WhenUpdated;
		IsDeleted = entry.IsDeleted;
		CorrelationId = entry.CorrelationId;
		Category = entry.Category;
		UserId = entry.UserId;
		IsMeeting = entry.IsMeeting;
	}
}
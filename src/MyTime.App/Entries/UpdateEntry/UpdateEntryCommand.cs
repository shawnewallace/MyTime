using System;
using MediatR;

namespace MyTime.App.Entries.UpdateEntry;

public class UpdateEntryCommand : IRequest<EntryModel>
{
  public Guid Id { get; set; }
  public DateTime? OnDate { get; set; }
  public string Description { get; set; }
  public float? Duration { get; set; }
  public bool? IsUtilization { get; set; }
  public string Category { get; set; }
  public string Notes { get; set; }
  public string CorrelationId { get; set; }
  public Guid? CategoryId { get; set; }
}

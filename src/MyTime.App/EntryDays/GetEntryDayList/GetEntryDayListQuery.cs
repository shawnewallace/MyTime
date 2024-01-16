using System;
using System.Collections.Generic;
using MediatR;

namespace MyTime.App.EntryDays.GetEntryDayList;

public class GetEntryDayListQuery : IRequest<EntryRangeModel>
{
  public DateTime? From { get; set; }
  public DateTime? To { get; set; }
  public bool IncludeDeleted { get; set; } = false;
}

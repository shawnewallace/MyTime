using System;
using System.Collections.Generic;
using MediatR;
using MyTime.App.Entries;

namespace Mytime.App.Entries.GetByCategory;

public class GetByCategoryQuery : IRequest<List<EntryModel>>
{
  public DateTime? From { get; set; }
  public DateTime? To { get; set; }
  public Guid? CategoryId { get; set; }
}

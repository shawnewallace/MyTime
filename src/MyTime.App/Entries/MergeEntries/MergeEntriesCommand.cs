using System;
using MediatR;
using MyTime.App.Models;

namespace MyTime.App.Entries.MergeEntries;
// public class MergeEntriesCommand : IRequest<EntryModel>
// {
// 	public Guid Primary { get; set; }
// 	public Guid Secondary { get; set; }
// }

public record MergeEntriesCommand(Guid Primary, Guid Secondary) : IRequest<EntryModel>;
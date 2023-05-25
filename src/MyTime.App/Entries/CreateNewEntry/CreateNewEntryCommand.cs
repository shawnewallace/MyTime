using System;
using MediatR;
using MyTime.App.Models;

namespace MyTime.App.Entries.CreateNewEntry
{
	public class CreateNewEntryCommand : IRequest<EntryModel>
	{
		public DateTime OnDate { get; set; }
		public string Description { get; set; }
		public float Duration { get; set; }
		public bool IsUtilization { get; set; }
		public string Category { get; set; } = string.Empty;
		public string Notes { get; set; }
		public string CorrelationId { get; set; } = String.Empty;
	}
}
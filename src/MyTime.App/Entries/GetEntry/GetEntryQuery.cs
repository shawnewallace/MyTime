using System;
using MediatR;

namespace MyTime.App.Entries.GetEntry
{
	public class GetEntryQuery : IRequest<EntryModel>
	{
		public Guid Id { get; private set; }

		public GetEntryQuery(Guid id) => Id = id;
	}
}
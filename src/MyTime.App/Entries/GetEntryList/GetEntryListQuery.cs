using System;
using System.Collections.Generic;
using MediatR;
using MyTime.App.Entries;

namespace Mytime.App.Entries.GetEntryList
{
	public class GetEntryListQuery : IRequest<List<EntryModel>>
	{
	}
}
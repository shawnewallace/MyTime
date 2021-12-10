using System.Collections.Generic;
using MediatR;
using MyTime.App.Models;

namespace Mytime.App.Entries.GetEntryList
{
	public class GetEntryListQuery : IRequest<List<EntryModel>>{}
}
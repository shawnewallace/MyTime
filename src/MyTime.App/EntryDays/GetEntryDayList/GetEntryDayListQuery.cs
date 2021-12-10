using System.Collections.Generic;
using MediatR;
using MyTime.App.Models;

namespace MyTime.App.EntryDays.GetEntryDayList
{
	public class GetEntryDayListQuery : IRequest<List<EntryDayModel>>
	{
		public int Year { get; set; }
		public int Month { get; set; }
		public int Day { get; set; }
	}
}
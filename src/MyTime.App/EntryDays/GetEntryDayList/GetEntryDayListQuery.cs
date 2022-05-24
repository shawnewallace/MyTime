using System;
using System.Collections.Generic;
using MediatR;
using MyTime.App.Models;

namespace MyTime.App.EntryDays.GetEntryDayList
{
	public class GetEntryDayListQuery : IRequest<EntryRangeModel>
	{
		public DateTime? From { get; set; }
		public DateTime? To { get; set; }
	}
}
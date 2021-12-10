using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyTime.App.Models;
using MyTime.Persistence;

namespace MyTime.App.EntryDays.GetEntryDayList
{
	public class GetEntryDayListQueryHandler : IRequestHandler<GetEntryDayListQuery, List<EntryDayModel>>
	{
		private readonly MyTimeSqlDbContext _context;

		public GetEntryDayListQueryHandler(MyTimeSqlDbContext context)
		{
			_context = context;
		}

		public async Task<List<EntryDayModel>> Handle(GetEntryDayListQuery request, CancellationToken cancellationToken)
		{
			var query = _context.Entries.Select(e => e);

			if (request.Year > 1) query = query.Where(e => e.OnDate.Year == request.Year);
			if (request.Month > 1) query = query.Where(e => e.OnDate.Month == request.Month);
			if (request.Day > 1) query = query.Where(e => e.OnDate.Day == request.Day);

			var result = await query.ToListAsync();

			var response = new List<EntryDayModel>();

			foreach(var thing in result.Select(m => m.OnDate).Distinct())
			{
				var newDay = new EntryDayModel(thing.Year, thing.Month, thing.Day);
				var newDayEntries = result.Where(m => m.OnDate.Year == newDay.Year && m.OnDate.Month == newDay.Month && m.OnDate.Day == newDay.DayOfMonth);

				foreach(var entry in newDayEntries)
				{
					newDay.Entries.Add(new EntryModel(entry));
				}

				response.Add(newDay);
			}

			return response;
		}
	}
}
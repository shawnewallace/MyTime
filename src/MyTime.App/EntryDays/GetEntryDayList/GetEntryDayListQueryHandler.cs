using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyTime.Persistence;

namespace MyTime.App.EntryDays.GetEntryDayList
{
	public class GetEntryDayListQueryHandler : IRequestHandler<GetEntryDayListQuery, EntryRangeModel>
	{
		private readonly MyTimeSqlDbContext _context;

		public GetEntryDayListQueryHandler(MyTimeSqlDbContext context)
		{
			_context = context;
		}

		public async Task<EntryRangeModel> Handle(GetEntryDayListQuery request, CancellationToken cancellationToken)
		{
			var query = _context.Entries.Select(e => e);
			DateTime from = DateTime.MinValue;
			DateTime to = DateTime.MaxValue;

			if (request.From.HasValue) {
				from = request.From.Value.Date;
				query = query.Where(e => e.OnDate >= from);
			}

			if (request.To.HasValue) {
				to = request.To.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
				query = query.Where(e => e.OnDate <= to);
			}

			if (!request.IncludeDeleted)
			{
				query = query.Where(e => !e.IsDeleted);
			}

			var result = await query.ToListAsync(cancellationToken);

			var response = new EntryRangeModel(from, to, result);

			return response;
		}
	}
}
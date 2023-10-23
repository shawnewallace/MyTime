using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyTime.Persistence;

namespace MyTime.App.Categories;

public class GetCategoryDaysQueryHandler : IRequestHandler<GetCategoryDaysQuery, List<CategoryDayModel>>
{
	private readonly MyTimeSqlDbContext _context;

	public GetCategoryDaysQueryHandler(MyTimeSqlDbContext context) => _context = context;

	public async Task<List<CategoryDayModel>> Handle(GetCategoryDaysQuery request, CancellationToken cancellationToken)
	{
		var query = _context.Entries.Select(m => new { m.Category, m.Duration, m.OnDate });
		DateTime from = DateTime.MinValue;
		DateTime to = DateTime.MaxValue;

		if (request.From.HasValue)
		{
			from = request.From.Value.Date;
			query = query.Where(e => e.OnDate >= from);
		}

		if (request.To.HasValue)
		{
			to = request.To.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
			query = query.Where(e => e.OnDate <= to);
		}

		var entries = await query
			.ToListAsync(cancellationToken);

		var result = new List<CategoryDayModel>();

		foreach (var entry in entries.Select(m => m.Category).Distinct().ToList())
		{
			result.Add(new CategoryDayModel { Name = entry });
		}

		foreach (var item in result)
		{
			item.Total = entries.Where(m => m.Category == item.Name).Sum(m => m.Duration);
			item.NumEntries = entries.Count(m => m.Category == item.Name);
			item.FirstEntry = entries.Min(m => m.OnDate);
			item.LastEntry = entries.Max(m => m.OnDate);
		}

		return result.OrderBy(m => m.Name).ToList();
	}
}

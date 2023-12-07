
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyTime.App.Infrastructure;
using MyTime.Persistence;

namespace MyTime.App.WeekSummary;

public class GetWeekSummaryQueryHandler(MyTimeSqlDbContext context) : IRequestHandler<GetWeekSummaryQuery, List<WeekSummaryModel>>
{
	private readonly MyTimeSqlDbContext _context = context;

	public async Task<List<WeekSummaryModel>> Handle(GetWeekSummaryQuery request, CancellationToken cancellationToken)
	{
		var bdCategories = await _context.Categories
			.Where(c => c.Name == "BD")
			.Include(c => c.Children)
			.ToListAsync(cancellationToken);

		var bdCategoryIds = bdCategories
			.SelectMany(c => c.Children!)
			.Select(c => c.Id)
			.ToList();

		var query = _context.Entries
			.Where(e => !e.IsDeleted)
			.Where(e =>
				e.OnDate >= request.From.FirstDayOfWeek()
				&& e.OnDate <= request.To.LastDayOfWeek());

		var rawEntries = await query.Select(e => new
		{
			e.OnDate,
			e.Duration,
			e.IsUtilization,
			e.IsMeeting,
			CategoryId = e.CategoryId.HasValue ? e.CategoryId.Value : Guid.Empty
		})
		.ToListAsync(cancellationToken);

		var results = rawEntries
			.GroupBy(e => new
			{
				Year = e.OnDate.Year,
				WeekNumber = CultureInfo
											.InvariantCulture
											.Calendar
											.GetWeekOfYear(
													e.OnDate,
													CalendarWeekRule.FirstDay,
													DayOfWeek.Sunday)
			})
			.Select(g => new WeekSummaryModel(
					g.Key.Year,
					g.Key.WeekNumber,
					new WeekOfYear(g.Key.Year, g.Key.WeekNumber).FirstDayOfWeek(),
					new WeekOfYear(g.Key.Year, g.Key.WeekNumber).LastDayOfWeek(),
					g.Sum(e => e.Duration),
					g.Sum(e => e.IsUtilization ? e.Duration : 0.0f),
					g.Sum(e => e.IsMeeting ? e.Duration : 0.0f),
					g.Sum(e => bdCategoryIds.Contains(e.CategoryId) ? e.Duration : 0.0f)
				))
			.OrderBy(g => g.Year)
			.ThenBy(g => g.WeekNumber);

		return [.. results];
	}
}


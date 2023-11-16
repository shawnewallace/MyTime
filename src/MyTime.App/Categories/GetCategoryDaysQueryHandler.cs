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
		var query = _context
			.Entries
			.Where(m => !m.IsDeleted)
			.Select(m => new { m.Category, m.Duration, m.OnDate, m.Description });
		
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

		var categoryNames = entries.Select(m => m.Category).Distinct().ToList();
		var result = new List<CategoryDayModel>();

		foreach (var categoryName in categoryNames)
		{
			var entriesForThisCategory = entries.Where(e => e.Category == categoryName).ToList();
			result.Add(new CategoryDayModel
			{
				Name = categoryName,
				Total = entriesForThisCategory.Sum(c => c.Duration),
				NumEntries = entriesForThisCategory.Count(),
				FirstEntry = entriesForThisCategory.Min(e => e.OnDate),
				LastEntry = entriesForThisCategory.Max(e => e.OnDate),
				Descriptions = string.Join(", ", entriesForThisCategory.Select(e => e.Description).ToArray())
			});
		}
		return result.OrderBy(m => m.Name).ToList();
	}
}

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
		var categories = await GetAllCategories(cancellationToken);

		var query = _context
			.Entries
			.Where(m => !m.IsDeleted)
			.Select(m => new { m.CategoryId, m.Duration, m.OnDate, m.Description });

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

		var entries = await query.ToListAsync(cancellationToken);

		var categoryIds = entries.Select(m => m.CategoryId).Distinct().ToList();
		var result = new List<CategoryDayModel>();

		foreach (var categoryId in categoryIds)
		{
			var categoryName = categories.FirstOrDefault(c => c.Id == categoryId)?.FullName ?? "Unknown";
			var entriesForThisCategory = entries.Where(e => e.CategoryId == categoryId).ToList();
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

	private async Task<List<CategoryModel>> GetAllCategories(CancellationToken cancellationToken)
	{
		var categories = await _context.Categories.Include(c => c.Parent).ToListAsync(cancellationToken);
		return categories.ConvertAll(c => new CategoryModel(
			c.Id,
			c.Name,
			c.IsDeleted,
			c.ParentId,
			c.Parent?.Name ?? ""
		));
	}
}

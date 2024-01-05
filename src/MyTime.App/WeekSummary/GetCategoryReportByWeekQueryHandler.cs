using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyTime.Persistence;
using MyTime.Persistence.Models;

namespace MyTime.App.WeekSummary;

public class GetCategoryReportByWeekQueryHandler(MyTimeSqlDbContext context) : IRequestHandler<GetCategoryReportByWeekQuery, List<CategorySummaryModel>>
{
	private readonly MyTimeSqlDbContext _context = context;

	public async Task<List<CategorySummaryModel>> Handle(GetCategoryReportByWeekQuery request, CancellationToken cancellationToken)
	{

		var raw = await _context.CategoryReportModels
			.FromSqlInterpolated($"EXECUTE dbo.GetCategoryReportByWeek {request.From.Date}, {request.To.Date}")
			.ToListAsync(cancellationToken);


		return raw.ConvertAll(r =>
			new CategorySummaryModel(
				r.Year,
				r.Week,
				r.ParentCategory,
				r.Category,
				r.Summary,
				r.TotalHours));
	}
}

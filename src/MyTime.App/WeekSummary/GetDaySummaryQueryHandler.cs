using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyTime.Persistence;

namespace MyTime.App.WeekSummary;

public class GetDaySummaryQueryHandler(MyTimeSqlDbContext context) : IRequestHandler<GetDaySummaryQuery, List<DaySummaryModel>>
{
	private readonly MyTimeSqlDbContext _context = context;

	public async Task<List<DaySummaryModel>> Handle(GetDaySummaryQuery request, CancellationToken cancellationToken)
	{
		var raw = await _context.DaySummaryModels
			.FromSqlInterpolated($"EXECUTE dbo.GetDailySummaryReport {request.From.Date}, {request.To.Date}")
			.ToListAsync(cancellationToken);

		return raw.ConvertAll(r =>
			new DaySummaryModel(
				r.OnDate,
				r.ParentCategory ?? string.Empty,
				r.Category ?? string.Empty,
				r.Duratation));
	}
}
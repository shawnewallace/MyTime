
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
    List<Persistence.Models.WeekSummaryModel> raw = await _context.WeekSummaryModels
      .FromSqlInterpolated($"EXECUTE dbo.GetSummaryReportByWeek {request.From.Date}, {request.To.Date}")
      .ToListAsync(cancellationToken);

    return raw.ConvertAll(r =>
      new WeekSummaryModel(
        r.Year,
        r.Week,
        r.TotalHours,
        r.UtilizedHours,
        r.MeetingHours,
        r.BusinessDevelopmentHours));
  }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyTime.App.Entries;
using MyTime.Persistence;
using MyTime.Persistence.Entities;

namespace Mytime.App.Entries.GetByCategory;

public class GetByCategoryQueryHandler : IRequestHandler<GetByCategoryQuery, List<EntryModel>>
{
  private readonly MyTimeSqlDbContext _context;

  public GetByCategoryQueryHandler(MyTimeSqlDbContext context)
  {
    _context = context;
  }

  public async Task<List<EntryModel>> Handle(GetByCategoryQuery request, CancellationToken cancellationToken)
  {
    IQueryable<Entry> query = _context.Entries
                  .Include(e => e.CategoryN)
                  .ThenInclude(c => c.Parent)
                  .Select(e => e);

    DateTime from = request.From ?? DateTime.MinValue;
    DateTime to = request.To ?? DateTime.MaxValue;

    query = query.Where(e => e.OnDate >= from && e.OnDate <= to);
    if (request.CategoryId.HasValue)
    {
      query = query.Where(e => e.CategoryId == request.CategoryId);
    }
    query = query.Where(e => !e.IsDeleted);

    List<Entry> result = await _context
                        .Entries
                        .ToListAsync(cancellationToken);

    var response = new List<EntryModel>();

    foreach (MyTime.Persistence.Entities.Entry? item in result)
    {
      response.Add(new EntryModel(item));
    }

    return response;
  }
}

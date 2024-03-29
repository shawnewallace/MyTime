using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyTime.App.Exceptions;
using MyTime.App.Infrastructure;
using MyTime.Persistence;
using MyTime.Persistence.Entities;

namespace MyTime.App.Entries.UpdateEntry;

public class UpdateEntryCommandHandler : IRequestHandler<UpdateEntryCommand, EntryModel>
{
  private readonly MyTimeSqlDbContext _context;

  public UpdateEntryCommandHandler(MyTimeSqlDbContext context)
  {
    _context = context;
  }
  public async Task<EntryModel> Handle(UpdateEntryCommand request, CancellationToken cancellationToken)
  {
    Entry? entry = await _context.Entries.FindAsync(request.Id) ?? throw new EntryNotFoundException(request.Id);

    if (request.OnDate.HasValue)
    {
      entry.OnDate = request.OnDate.Value;
    }

    if (!string.IsNullOrWhiteSpace(request.Description))
    {
      entry.Description = request.Description;
    }

    if (request.Duration.HasValue)
    {
      entry.Duration = request.Duration.Value;
    }

    if (request.IsUtilization.HasValue)
    {
      entry.IsUtilization = request.IsUtilization.Value;
    }

    if (request.CategoryId.HasValue)
    {
      entry.CategoryId = request.CategoryId.Value;
    }

    if (!string.IsNullOrWhiteSpace(request.Notes))
    {
      entry.Notes = request.Notes;
    }

    if (!string.IsNullOrWhiteSpace(request.CorrelationId))
    {
      entry.CorrelationId = request.CorrelationId;
    }

    entry.WhenUpdated = DateTime.UtcNow;

    await _context.SaveChangesAsync(cancellationToken);

    return new EntryModel(entry);
  }
}

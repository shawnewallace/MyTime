using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyTime.App.Infrastructure;
using MyTime.Persistence;
using MyTime.Persistence.Entities;

namespace MyTime.App.Entries.CreateNewEntry;

public class CreateNewEntryCommandHandler : IRequestHandler<CreateNewEntryCommand, EntryModel>
{
  private readonly MyTimeSqlDbContext _context;

  public CreateNewEntryCommandHandler(MyTimeSqlDbContext context)
  {
    _context = context;
  }
  public async Task<EntryModel> Handle(CreateNewEntryCommand request, CancellationToken cancellationToken)
  {
    // correlation id should be unique
    if (!string.IsNullOrEmpty(request.CorrelationId) & _context.Entries.Any(e => e.CorrelationId == request.CorrelationId))
    {
      throw new DuplicateCorrelationIdException(request.CorrelationId);
    }


    var newEntry = new Entry
    {
      OnDate = request.OnDate,
      Description = request.Description.Length > 255 ? request.Description[..255] : request.Description,
      Duration = request.Duration,
      IsUtilization = request.IsUtilization,
      Notes = request.Notes,
      CorrelationId = request.CorrelationId,
      CategoryId = request.CategoryId,
      UserId = request.UserId,
      IsMeeting = request.IsMeeting
    };

    await _context.Entries.AddAsync(newEntry, cancellationToken);

    await _context.SaveChangesAsync(cancellationToken);

    return new EntryModel(newEntry);
  }
}

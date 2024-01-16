using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyTime.App.Exceptions;
using MyTime.Persistence;
using MyTime.Persistence.Entities;

namespace MyTime.App.Entries.MergeEntries;
public class MergeEntriesCommandHandler : IRequestHandler<MergeEntriesCommand, EntryModel>
{
  private readonly MyTimeSqlDbContext _context;

  public MergeEntriesCommandHandler(MyTimeSqlDbContext context)
  {
    _context = context;
  }
  public async Task<EntryModel> Handle(MergeEntriesCommand request, CancellationToken cancellationToken)
  {
    Entry? primaryEntry = await _context.Entries.FindAsync(request.Primary);
    Entry? secondaryEntry = await _context.Entries.FindAsync(request.Secondary);

    if (primaryEntry is null)
    {
      throw new ValidationException();
    }

    if (primaryEntry.IsDeleted)
    {
      throw new ValidationException();
    }

    if (secondaryEntry is null)
    {
      throw new ValidationException();
    }

    if (secondaryEntry.IsDeleted)
    {
      throw new ValidationException();
    }

    if (primaryEntry.OnDate != secondaryEntry.OnDate)
    {
      throw new ValidationException();
    }

    // primaryEntry.OnDate is not modified
    // primaryEntry.Description is not modified
    // duration should be the sum of the two values
    primaryEntry.Duration += secondaryEntry.Duration;
    // if either is billable, the resulting entry is billable
    primaryEntry.IsUtilization = primaryEntry.IsUtilization || secondaryEntry.IsUtilization;
    // primaryEntry.Notes is not modified
    // primaryEntry.CorrelationId is not modified
    // primaryEntry.Category is not modified

    // delete secondary maggesgt
    secondaryEntry.IsDeleted = true;
    await _context.SaveChangesAsync(cancellationToken);

    return new EntryModel(primaryEntry);
  }
}

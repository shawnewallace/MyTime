using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyTime.Persistence;
using MyTime.Persistence.Entities;

namespace MyTime.App.Entries.GetEntry;

public class CreateNewEntryCommandHandler : IRequestHandler<GetEntryQuery, EntryModel>
{
  private readonly MyTimeSqlDbContext _context;

  public CreateNewEntryCommandHandler(MyTimeSqlDbContext context) => _context = context;
  public async Task<EntryModel> Handle(GetEntryQuery query, CancellationToken cancellationToken)
  {
    Entry? result = await _context.Entries.FindAsync(query.Id, cancellationToken);

    if (result is null)
    {
      return null;
    }

    return new EntryModel(result);
  }
}

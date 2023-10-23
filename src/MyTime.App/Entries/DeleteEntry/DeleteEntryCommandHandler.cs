using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyTime.App.Infrastructure;
using MyTime.Persistence;
using MyTime.Persistence.Entities;

namespace MyTime.App.Entries.DeleteEntry;

public class DeleteEntryCommandHandler : IRequestHandler<DeleteEntryCommand>
{
	private readonly MyTimeSqlDbContext _context;

	public DeleteEntryCommandHandler(MyTimeSqlDbContext context)
	{
		_context = context;
	}
	public async Task Handle(DeleteEntryCommand request, CancellationToken cancellationToken)
	{
		var entry = _context.Entries.FirstOrDefault(x => x.Id == request.Id && x.UserId == request.UserId);

		if (entry is null) throw new EntryNotFoundException(request.Id);

		entry.IsDeleted = true;
		entry.WhenUpdated = DateTime.UtcNow;

		await _context.SaveChangesAsync(cancellationToken);
	}
}
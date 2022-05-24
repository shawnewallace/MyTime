using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyTime.App.Models;
using MyTime.Persistence;
using MyTime.Persistence.Entities;

namespace MyTime.App.Entries.CreateNewEntry
{
	public class CreateNewEntryCommandHandler : IRequestHandler<CreateNewEntryCommand, EntryModel>
	{
		private readonly MyTimeSqlDbContext _context;

		public CreateNewEntryCommandHandler(MyTimeSqlDbContext context) { 
			_context = context;
		}
		public async Task<EntryModel> Handle(CreateNewEntryCommand request, CancellationToken cancellationToken)
		{
			var newEntry = new Entry();
			
			newEntry.OnDate = request.OnDate;
			newEntry.Description = request.Description;
			newEntry.Duration = request.Duration;
			newEntry.IsUtilization = request.IsUtilization;
			newEntry.Notes = request.Notes;

			await _context.Entries.AddAsync(newEntry, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);

			return new EntryModel(newEntry);
		}
	}
}
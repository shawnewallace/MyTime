using System;
using System.Linq;
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

		public CreateNewEntryCommandHandler(MyTimeSqlDbContext context)
		{
			_context = context;
		}
		public async Task<EntryModel> Handle(CreateNewEntryCommand request, CancellationToken cancellationToken)
		{
			var newEntry = new Entry
			{
				OnDate = request.OnDate,
				Description = request.Description.Length > 255 ? request.Description[..255] : request.Description,
				Duration = request.Duration,
				IsUtilization = request.IsUtilization,
				Notes = request.Notes,
				CorrelationId = request.CorrelationId,
				Category = request.Category,
				UserId = request.UserId
			};

			await _context.Entries.AddAsync(newEntry, cancellationToken);

			if (ShouldCreateCategory(request.Category))
			{
				var newCategory = new Category
				{
					Name = request.Category
				};
				await _context.Categories.AddAsync(newCategory, cancellationToken);
			}

			await _context.SaveChangesAsync(cancellationToken);

			return new EntryModel(newEntry);
		}

		private bool ShouldCreateCategory(string category)
		{
			if (string.IsNullOrWhiteSpace(category)) return false;
			if (_context.Categories.Any(c => c.Name.ToLower() == category.ToLower())) return false;

			return true;
		}
	}
}
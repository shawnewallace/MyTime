using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyTime.App.Infrastructure;
using MyTime.Persistence;

namespace MyTime.App.Categories.ToggleActive
{
	public class ToggleActiveCommandHandler : IRequestHandler<ToggleActiveCommand>
	{
		private readonly MyTimeSqlDbContext _context;

		public ToggleActiveCommandHandler(MyTimeSqlDbContext context) => _context = context;
		public async Task Handle(ToggleActiveCommand request, CancellationToken cancellationToken)
		{
			var category = await _context.Categories.FindAsync(request.Id, cancellationToken);
			if (category is null) throw new CategoryNotFoundException(request.Id);

			category.IsDeleted = !category.IsDeleted;
			category.WhenUpdated = DateTime.UtcNow;

			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
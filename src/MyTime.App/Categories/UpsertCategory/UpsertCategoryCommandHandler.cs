using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyTime.App.Models;
using MyTime.Persistence;
using MyTime.Persistence.Entities;

namespace MyTime.App.Categories.UpsertCategory
{
	public class UpsertCategoryCommandHandler : IRequestHandler<UpsertCategoryEntryCommand, Category>
	{
		private readonly MyTimeSqlDbContext _context;

		public UpsertCategoryCommandHandler(MyTimeSqlDbContext context) => _context = context;
		public async Task<Category> Handle(UpsertCategoryEntryCommand request, CancellationToken cancellationToken)
		{
			var category = await _context
											.Categories
											.Where(c => c.Name == request.Name)
											.FirstOrDefaultAsync(cancellationToken: cancellationToken);

			if (category is null)
			{
				category = new Category
				{
					Name = request.Name
				};
				await _context.Categories.AddAsync(category);
			}

			category.WhenUpdated = System.DateTime.UtcNow;
			await _context.SaveChangesAsync(cancellationToken);

			return category;
		}
	}
}
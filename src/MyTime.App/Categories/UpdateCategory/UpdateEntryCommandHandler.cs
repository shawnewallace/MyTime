using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyTime.App.Exceptions;
using MyTime.App.Infrastructure;
using MyTime.App.Models;
using MyTime.Persistence;
using MyTime.Persistence.Entities;

namespace MyTime.App.Categories.UpdateCategory
{
	public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Category>
	{
		private readonly MyTimeSqlDbContext _context;

		public UpdateCategoryCommandHandler(MyTimeSqlDbContext context)
		{
			_context = context;
		}
		public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
		{
			var category = await _context.Categories.FindAsync(request.Id, cancellationToken);
			if (category is null) throw new CategoryNotFoundException(request.Id);

			var oldCategoryName = category.Name;

			category.Name = request.Name;
			category.WhenUpdated = DateTime.UtcNow;

			await _context.SaveChangesAsync(cancellationToken);

			_context.Entries
				.Where(e => e.Category == oldCategoryName)
				.ExecuteUpdate(e => e.SetProperty(s => s.Category, category.Name));

			return category;
		}
	}
}
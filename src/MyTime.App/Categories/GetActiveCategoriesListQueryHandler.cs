using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MyTime.Persistence;
using MyTime.Persistence.Entities;

namespace MyTime.App.Categories;

public class GetActiveCategoriesListQueryHandler : IRequestHandler<GetActiveCategoriesListQuery, List<CategoryModel>>
{
	private readonly MyTimeSqlDbContext _context;

	public GetActiveCategoriesListQueryHandler(MyTimeSqlDbContext context) => _context = context;

	public async Task<List<CategoryModel>> Handle(GetActiveCategoriesListQuery request, CancellationToken cancellationToken)
	{
		var categories = await _context.Categories
			.Include(c => c.Parent)
			.Where(c => !c.IsDeleted)
			.ToListAsync(cancellationToken: cancellationToken);

		return categories.ConvertAll(c => new CategoryModel(
			c.Id,
			c.Name,
			c.IsDeleted,
			c.ParentId,
			c.Parent?.Name ?? ""
		));
	}
}

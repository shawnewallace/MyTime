using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyTime.Persistence;
using MyTime.Persistence.Entities;

namespace MyTime.App.Categories;

public class GetAllCategoriesListQueryHandler : IRequestHandler<GetAllCategoriesListQuery, List<Category>>
{

	private readonly MyTimeSqlDbContext _context;

	public GetAllCategoriesListQueryHandler(MyTimeSqlDbContext context) => _context = context;

	public async Task<List<Category>> Handle(GetAllCategoriesListQuery request, CancellationToken cancellationToken) =>
		await _context
			.Categories
			.OrderBy(c => c.IsDeleted)
			.ThenBy(c => c.Name)
			.ToListAsync(cancellationToken: cancellationToken);
}
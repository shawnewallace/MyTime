using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyTime.Persistence;
using MyTime.Persistence.Entities;

namespace MyTime.App.Categories;

public class GetActiveCategoriesListQueryHandler : IRequestHandler<GetActiveCategoriesListQuery, List<Category>>
{
	private readonly MyTimeSqlDbContext _context;

	public GetActiveCategoriesListQueryHandler(MyTimeSqlDbContext context) => _context = context;

	public async Task<List<Category>> Handle(GetActiveCategoriesListQuery request, CancellationToken cancellationToken) => await _context.Categories.Where(c => !c.IsDeleted).OrderBy(c => c.Name).ToListAsync();
}
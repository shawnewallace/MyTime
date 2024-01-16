using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyTime.Persistence;
using MyTime.Persistence.Entities;

namespace MyTime.App.Categories;

public class GetAllCategoriesListQueryHandler : IRequestHandler<GetAllCategoriesListQuery, List<CategoryModel>>
{

  private readonly MyTimeSqlDbContext _context;

  public GetAllCategoriesListQueryHandler(MyTimeSqlDbContext context) => _context = context;

  public async Task<List<CategoryModel>> Handle(GetAllCategoriesListQuery request, CancellationToken cancellationToken)
  {
    List<Category> categories = await _context
      .Categories
      .Include(c => c.Parent)
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

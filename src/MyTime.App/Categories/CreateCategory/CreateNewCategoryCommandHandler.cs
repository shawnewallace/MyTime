using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyTime.Persistence;
using MyTime.Persistence.Entities;

namespace MyTime.App.Categories.CreateNewCategory;

public class CreateNewCategoryCommandHandler : IRequestHandler<CreateNewCategoryCommand, Category>
{
  private readonly MyTimeSqlDbContext _context;

  public CreateNewCategoryCommandHandler(MyTimeSqlDbContext context)
  {
    _context = context;
  }
  public async Task<Category> Handle(CreateNewCategoryCommand request, CancellationToken cancellationToken)
  {
    var newCategory = new Category
    {
      Name = request.Name,
      IsDeleted = request.IsDeleted
    };

    if (request.ParentId is not null)
    {
      var parentId = new Guid(request.ParentId);
      newCategory.ParentId = parentId;
    }

    await _context.Categories.AddAsync(newCategory, cancellationToken);

    await _context.SaveChangesAsync(cancellationToken);

    return newCategory;
  }
}

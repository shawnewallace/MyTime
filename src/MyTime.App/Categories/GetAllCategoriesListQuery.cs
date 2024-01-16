using System.Collections.Generic;
using MediatR;
using MyTime.Persistence.Entities;

namespace MyTime.App.Categories;

public class GetAllCategoriesListQuery : IRequest<List<CategoryModel>> { }

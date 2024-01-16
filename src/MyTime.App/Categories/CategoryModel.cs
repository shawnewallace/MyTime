using System;
using Microsoft.EntityFrameworkCore.Internal;

namespace MyTime.App.Categories;

public record CategoryModel(
  Guid Id,
  string Name,
  bool IsDeleted,
  Guid? ParentId = null,
  string ParentName = ""
)
{
  public string FullName => string.IsNullOrEmpty(ParentName) ? Name : $"{ParentName}:{Name}";
};

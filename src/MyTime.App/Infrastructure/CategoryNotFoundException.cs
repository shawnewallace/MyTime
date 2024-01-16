using System;

namespace MyTime.App.Infrastructure;

public class CategoryNotFoundException : Exception
{
  public CategoryNotFoundException(Guid id) : base("Category " + id + " not found.") { }
}

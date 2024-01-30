using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyTime.Persistence.Infrastructure;

namespace MyTime.Persistence.Entities;

public class Category : AppEntityBase
{
  [StringLength(50)] public string Name { get; set; } = string.Empty;
  public Guid? ParentId { get; set; }
  public Category? Parent { get; set; }
  public List<Category>? Children { get; set; }
  public Guid? UserId { get; set; }
  public User? User { get; set; }
}

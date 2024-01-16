using System;

namespace MyTime.Persistence.Infrastructure;

public abstract class AppEntityBase : IEntity<Guid>
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public DateTime WhenCreated { get; set; } = DateTime.UtcNow;
  public DateTime WhenUpdated { get; set; } = DateTime.UtcNow;
  public bool IsDeleted { get; set; } = false;
}

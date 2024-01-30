using MyTime.Persistence.Infrastructure;

namespace MyTime.Persistence.Entities;

public class User : AppEntityBase
{
  public string SSOId { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
}

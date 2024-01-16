using System;

namespace MyTime.Persistence.Models;

public sealed class DaySummaryModel
{
  public DateTime OnDate { get; set; }
  public string? ParentCategory { get; set; } = null!;
  public string? Category { get; set; } = null!;
  public string? Description { get; set; } = null!;
  public double Duratation { get; set; }
}

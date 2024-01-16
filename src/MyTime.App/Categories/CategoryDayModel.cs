using System;

namespace MyTime.App.Categories;

public class CategoryDayModel
{
  public string Name { get; set; }
  public float Total { get; set; }
  public int NumEntries { get; set; }
  public DateTime FirstEntry { get; set; }
  public DateTime LastEntry { get; set; }
  public string Descriptions { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using MyTime.App.Categories;
using MyTime.App.Entries;
using MyTime.App.Infrastructure;
using MyTime.Persistence.Entities;

namespace MyTime.App.EntryDays;

public class EntryRangeModel : IEntryRollup
{
  public DateTime RangeStart { get; private set; }
  public DateTime RangeEnd { get; private set; }

  public float Total => Days.Sum(m => m.Total);

  public float UtilizedTotal => Days.Sum(m => m.UtilizedTotal);

  public int NumEntries { get; private set; }
  public int NumCategories => Categories.Count();

  public List<EntryModel> Entries { get; private set; } = new();
  public List<EntryDayModel> Days { get; private set; } = new();
  public List<CategoryDayModel> Categories { get; private set; } = new();

  public EntryRangeModel()
  {
  }

  public EntryRangeModel(DateTime from, DateTime to, List<Entry> entries)
  {
    RangeStart = from;
    RangeEnd = to;
    NumEntries = entries.Count();

    FillDays(entries);
    FillEntries(entries);
    FillCategories(entries);
  }

  private void FillCategories(List<Entry> entries)
  {
    var categoryNames = entries.Select(m => m.CategoryN?.Name ?? "").Distinct().ToList();

    foreach (string? categoryName in categoryNames)
    {
      var entriesForThisCategory = entries.Where(e => (e.CategoryN?.Name ?? "") == categoryName).ToList();
      Categories.Add(new CategoryDayModel
      {
        Name = categoryName,
        Total = entriesForThisCategory.Sum(c => c.Duration),
        NumEntries = entriesForThisCategory.Count(),
        FirstEntry = entriesForThisCategory.Min(e => e.OnDate),
        LastEntry = entriesForThisCategory.Max(e => e.OnDate)
      });
    }
  }

  private void FillEntries(List<Entry> entries)
  {
    foreach (Entry? entry in entries.OrderBy(m => m.OnDate).ThenBy(m => m.WhenCreated))
    {
      Entries.Add(new EntryModel(entry));
    }
  }

  private void FillDays(List<Entry> entries)
  {
    DateTime iterator = RangeStart;

    while (iterator <= RangeEnd)
    {
      var newDay = new EntryDayModel(iterator.Year, iterator.Month, iterator.Day);
      IEnumerable<Entry> entriesForDay = entries.Where(m =>
        m.OnDate.Year == newDay.Year && m.OnDate.Month == newDay.Month && m.OnDate.Day == newDay.DayOfMonth);

      newDay.Total = entriesForDay.Sum(m => m.Duration);
      newDay.UtilizedTotal = entriesForDay.Where(m => m.IsUtilization).Sum(m => m.Duration);
      newDay.NumEntries = entriesForDay.Count();

      Days.Add(newDay);

      iterator = iterator.AddDays(1);
    }
  }
}

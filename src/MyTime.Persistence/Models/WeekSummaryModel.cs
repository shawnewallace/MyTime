namespace MyTime.Persistence.Models;

public sealed class WeekSummaryModel
{
  public int Year { get; set; }
  public int Week { get; set; }
  public double TotalHours { get; set; }
  public double UtilizedHours { get; set; }
  public double MeetingHours { get; set; }
  public double BusinessDevelopmentHours { get; set; }
}

namespace MyTime.Api.Models;

public class DateRangeModel
{
  public DateTime From { get; set; } = new DateTime(DateTime.Now.Year, 1, 1);
  public DateTime To { get; set; } = DateTime.Now;
}

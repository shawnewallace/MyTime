namespace MyTime.Api.Models;

public class AppointmentStartingNotificationModel
{
  public string eventId { get; set; } = string.Empty;
  public string subject { get; set; } = string.Empty;
  public DateTime startTime { get; set; }
  public DateTime? endTime { get; set; }
  public string name { get; set; } = string.Empty;
}

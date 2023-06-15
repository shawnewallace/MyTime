namespace MyTime.Api.Models;

public class AppointmentStartingNotificationModel
{
	public string eventId { get; set; } = String.Empty;
	public string subject { get; set; } = string.Empty;
	public DateTime startTime { get; set; }
	public string name { get; set; } = String.Empty;
}
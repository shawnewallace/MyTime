namespace MyTime.App.Models
{
	public class Entry
	{
		public string Description { get; set; }
		public float Duration { get; set; } = 0.0F;
		public bool IsUtilization { get; set; } = true;
		public string Notes { get; set; }
	}
}

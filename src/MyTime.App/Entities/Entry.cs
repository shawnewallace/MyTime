using MyTime.App.Infrastructure;

namespace MyTime.App.Entities
{
	public class Entry : AppEntityBase
	{
		public string Description { get; set; }
		public float Duration { get; set; } = 0.0F;
		public bool IsUtilization { get; set; } = true;
		public string Notes { get; set; }
	}
}

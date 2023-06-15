using System;


namespace MyTime.Persistence.Infrastructure
{
	public interface IEntry
	{
		DateTime OnDate { get; set; }
		string Description { get; set; }
		string Category { get; set; }
		float Duration { get; set; }
		bool IsUtilization { get; set; }
		string Notes { get; set; }
	}
}
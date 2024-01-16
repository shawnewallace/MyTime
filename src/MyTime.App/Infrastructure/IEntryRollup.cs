namespace MyTime.App.Infrastructure;

public interface IEntryRollup
{
  float Total { get; }
  float UtilizedTotal { get; }
  int NumEntries { get; }
}

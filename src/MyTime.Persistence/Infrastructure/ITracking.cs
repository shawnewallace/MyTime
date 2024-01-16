using System;


namespace MyTime.Persistence.Infrastructure;

public interface ITracking
{
  DateTime WhenCreated { get; set; }
  DateTime WhenUpdated { get; set; }
  bool IsDeleted { get; set; }
}

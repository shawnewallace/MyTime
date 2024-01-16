using System;

namespace MyTime.App.Infrastructure;

public class EntryNotFoundException : Exception
{
  public EntryNotFoundException(Guid id) : base("Entry " + id + " not found.") { }
}

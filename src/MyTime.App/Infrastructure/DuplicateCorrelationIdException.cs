using System;

namespace MyTime.App.Infrastructure;

public class DuplicateCorrelationIdException : Exception
{
  public DuplicateCorrelationIdException(string correlationId) : base("CorrelationId " + correlationId + " already exists.") { }
}

using System;
using MyTime.Persistence;

namespace MyTime.Integration.Tests.Infrastructure;

public class QueryTestFixture : IDisposable
{
  public MyTimeSqlDbContext Context { get; set; }

  public QueryTestFixture()
  {
    Context = MyTimeSqlDbContextFactory.Create();
  }

  public void Dispose()
  {
    MyTimeSqlDbContextFactory.Destroy(Context);
  }
}

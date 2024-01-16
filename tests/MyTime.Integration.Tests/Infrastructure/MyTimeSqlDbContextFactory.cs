using System;
using Microsoft.EntityFrameworkCore;
using MyTime.Persistence;
using MyTime.Persistence.Entities;

namespace MyTime.Integration.Tests.Infrastructure;

public class MyTimeSqlDbContextFactory
{
  public static MyTimeSqlDbContext Create()
  {
    DbContextOptions<MyTimeSqlDbContext> options = new DbContextOptionsBuilder<MyTimeSqlDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

    var context = new MyTimeSqlDbContext(options);

    context.Database.EnsureCreated();

    context.Entries.AddRange(new[] {
              new Entry{Id = Guid.NewGuid(), OnDate = new DateTime(2021, 10, 21), Description = $"Description - {Guid.NewGuid().ToString()}"},
              new Entry{Id = Guid.NewGuid(), OnDate = new DateTime(2021, 12, 14), Description = $"Description - {Guid.NewGuid().ToString()}"},
              new Entry{Id = Guid.NewGuid(), OnDate = new DateTime(2021, 12, 14), Description = $"Description - {Guid.NewGuid().ToString()}"}
          });

    context.SaveChanges();

    return context;
  }

  public static void Destroy(MyTimeSqlDbContext context)
  {
    if (!context.Database.IsInMemory())
    {
      return;
    }

    context.Database.EnsureDeleted();

    context.Dispose();
  }
}

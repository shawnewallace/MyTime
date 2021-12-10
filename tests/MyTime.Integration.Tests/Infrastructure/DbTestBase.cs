using System;
using Microsoft.EntityFrameworkCore;
using MyTime.Persistence;

namespace MyTime.Integration.Tests.Infrastructure
{
	public abstract class DbTestBase
	{
		public MyTimeSqlDbContext GetDbContext(bool useSqlLite = false)
		{
			var builder = new DbContextOptionsBuilder<MyTimeSqlDbContext>();

			if (useSqlLite) { builder.UseSqlite("DataSource=:memory:", x => { }); }
			else { builder.UseInMemoryDatabase(Guid.NewGuid().ToString()); }

			var dbContext = new MyTimeSqlDbContext(builder.Options);
			if (useSqlLite) { dbContext.Database.OpenConnection(); }

			dbContext.Database.EnsureCreated();

			return dbContext;
		}
	}
}
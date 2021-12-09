using System;
using Microsoft.EntityFrameworkCore;
using MyTime.App.Entities;

namespace MyTime.Persistence
{
	public class MyTimeSqlDbContext : DbContext
	{
		public DbSet<Entry> Entries { get; set; }

		public MyTimeSqlDbContext() { }
		public MyTimeSqlDbContext(DbContextOptions<MyTimeSqlDbContext> options) : base(options) { }
	}
}

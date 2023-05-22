using System;
using Microsoft.EntityFrameworkCore;
using MyTime.Persistence.Entities;

namespace MyTime.Persistence
{
	public class MyTimeSqlDbContext : DbContext
	{
		public DbSet<Entry> Entries { get; set; }
		public DbSet<Category> Categories { get; set; }

		public MyTimeSqlDbContext(DbContextOptions<MyTimeSqlDbContext> options) : base(options) { }
	}
}
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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Entry>().HasIndex(e => e.OnDate);
			modelBuilder.Entity<Entry>()
				.HasOne(e => e.CategoryN)
				.WithMany()
				.HasForeignKey(e => e.CategoryId);


			modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();

			modelBuilder.Entity<Category>()
				.HasOne(c => c.Parent)
				.WithMany(c => c!.Children)
				.HasForeignKey(c => c.ParentId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
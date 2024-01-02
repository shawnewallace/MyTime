using System;
using Microsoft.EntityFrameworkCore;
using MyTime.Persistence.Entities;
using MyTime.Persistence.Models;

namespace MyTime.Persistence
{
	public class MyTimeSqlDbContext : DbContext
	{
		public DbSet<Entry> Entries { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<CategoryReportModel> CategoryReportModels { get; set; } = null!;
		public DbSet<WeekSummaryModel> WeekSummaryModels { get; set; } = null!;

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

			modelBuilder.Entity<CategoryReportModel>().HasNoKey().ToView(null);
			modelBuilder.Entity<WeekSummaryModel>().HasNoKey().ToView(null);
		}
	}
}
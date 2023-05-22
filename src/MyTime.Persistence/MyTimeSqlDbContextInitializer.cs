using System.Collections.Generic;
using System.Linq;
using MyTime.Persistence.Entities;

namespace MyTime.Persistence
{
	public class MyTimeSqlDbContextInitializer
	{
		public static void Initialize(MyTimeSqlDbContext context)
		{
			var initializer = new MyTimeSqlDbContextInitializer();
			initializer.Seed(context);
		}

		private void Seed(MyTimeSqlDbContext context)
		{
			context.Database.EnsureCreated();

			List<string> defaultCategories = new() { "Tech SL:Admin", "Anvil", "Heamonetics", "Church of Christ, Scientist", "Tech Council", "Race Winning Brands", "Training", "Camp I/O", "Leadership"};

			foreach(var category in defaultCategories)
			{
				if (context.Categories.Any(m => m.Name == category)) continue;

				Category newCategory = new() { 
					 Name = category
				};

				context.Add(newCategory);
			}

			context.SaveChanges();

		}
	}
}

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
		}
	}
}

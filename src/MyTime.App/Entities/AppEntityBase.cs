using System;

namespace MyTime.App.Infrastructure
{
	public abstract class AppEntityBase : IEntity<Guid>
	{
		public Guid Id { get; set; }
		public DateTime WhenCreated { get; set; } = DateTime.UtcNow;
		public DateTime WhenUpdated { get; set; } = DateTime.UtcNow;
		public bool IsDeleted { get; set; } = false;
	}
}
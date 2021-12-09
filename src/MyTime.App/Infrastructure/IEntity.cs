using System;

namespace MyTime.App.Infrastructure
{
	public interface IEntity<TKey>
	{
		TKey Id { get; set; }
		DateTime WhenCreated { get; set; }
		DateTime WhenUpdated { get; set; }
		bool IsDeleted {get;set;}
	}
}
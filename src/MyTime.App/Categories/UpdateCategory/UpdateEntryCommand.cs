using System;
using MediatR;
using MyTime.Persistence.Entities;

namespace MyTime.App.Categories.UpdateCategory
{
	public class UpdateCategoryCommand : IRequest<Category>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}
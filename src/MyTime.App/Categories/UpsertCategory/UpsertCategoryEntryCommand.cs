using System;
using MediatR;
using MyTime.App.Models;
using MyTime.Persistence.Entities;

namespace MyTime.App.Categories.UpsertCategory
{
	public class UpsertCategoryEntryCommand : IRequest<Category>
	{
		public string Name { get; set; }
	}
}
using System;
using MediatR;
using MyTime.Persistence.Entities;

namespace MyTime.App.Categories.CreateNewCategory;

public class CreateNewCategoryCommand : IRequest<Category>
{
	public string Name { get; set; }
	public bool IsDeleted { get; set; }
}
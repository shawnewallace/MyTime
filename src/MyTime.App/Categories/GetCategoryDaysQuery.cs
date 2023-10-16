using System;
using System.Collections.Generic;
using MediatR;
using MyTime.App.Models;

namespace MyTime.App.Categories;

public class GetCategoryDaysQuery : IRequest<List<CategoryDayModel>>
{
	public DateTime? From { get; set; }
	public DateTime? To { get; set; }
}
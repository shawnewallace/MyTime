using System;
using System.Collections.Generic;
using MediatR;

namespace MyTime.App.Categories;

public class GetCategoryDaysQuery : IRequest<List<CategoryDayModel>>
{
	public DateTime? From { get; set; }
	public DateTime? To { get; set; }
}
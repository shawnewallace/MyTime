using System.Collections.Generic;
using MediatR;

namespace MyTime.App.WeekSummary;

public class GetWeekSummaryQuery : IRequest<List<WeekSummaryModel>>
{
	public int? Year { get; set; }
}


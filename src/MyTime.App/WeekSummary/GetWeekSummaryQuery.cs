using System;
using System.Collections.Generic;
using MediatR;

namespace MyTime.App.WeekSummary;

// public class GetWeekSummaryQuery : IRequest<List<WeekSummaryModel>>
// {
// 	public class GetWeekSummaryQuery(DateTime from, DateTime to)
// 	{
// 		From = from;
// 		To = to;
// 	}
// 	public int? Year { get; set; }
// }

public record GetWeekSummaryQuery(DateTime From, DateTime To) : IRequest<List<WeekSummaryModel>>;


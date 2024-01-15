using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyTime.Api.Infrastructure;
using MyTime.Api.Models;
using MyTime.App.WeekSummary;

namespace MyTime.Api.Reporting;

public class ReportEndpoints : EndpointBase, ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("report").WithName("report-endpoints");
		group.MapPost("week-summary", WeekSummary).WithName("week-summary");
		group.MapPost("category-summary-by-week/{from}/{to}", CategorySummaryByWeek).WithName("category-summary-by-week");
		group.MapPost("category-summary-by-day/{from}/{to}", CategorySummaryByDay).WithName("category-summary-by-day");
	}

	public static async Task<IResult> WeekSummary(
		[FromBody] DateRangeModel model,
		IMediator mediator)
		=> Results.Ok(await mediator.Send(request: new GetWeekSummaryQuery(model.From, model.To)));

	public static async Task<IResult> CategorySummaryByWeek(string from, string to, IMediator mediator)
	{
		var start = DateTime.Parse(from);
		var end = DateTime.Parse(to);

		var result = await mediator.Send(request: new GetCategoryReportByWeekQuery(start, end));

		return Results.Ok(result);
	}

	public static async Task<IResult> CategorySummaryByDay(string from, string to, IMediator mediator)
	{
		var start = DateTime.Parse(from);
		var end = DateTime.Parse(to);

		var result = await mediator.Send(request: new GetDaySummaryQuery(start, end));

		return Results.Ok(result.OrderBy(x => x.OnDate).ThenBy(x => x.FullName));
	}
}


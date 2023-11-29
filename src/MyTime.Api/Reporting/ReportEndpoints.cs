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
	}

	public static async Task<IResult> WeekSummary(
		[FromBody] DateRangeModel model,
		IMediator mediator)
		=> Results.Ok(await mediator.Send(request: new GetWeekSummaryQuery(model.From, model.To)));
}

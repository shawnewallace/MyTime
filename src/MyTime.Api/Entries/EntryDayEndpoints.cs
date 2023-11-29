using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyTime.Api.Models;
using MyTime.App.EntryDays.GetEntryDayList;
using MyTime.App.Infrastructure;
using MyTime.App.EntryDays;
using MyTime.Api.Infrastructure;
using Carter;

namespace MyTime.Api.Controllers;

public class EntryDayEndpoints : EndpointBase, ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/day", Day).WithName("get-entry-day");
		app.MapPost("/entries/between/{from}/{to}", GetRange).WithName("get-range-of-days");
		app.MapPost("/week/{dayOfWeek}", GetWeekContainingDate).WithName("get-week-containing-date");
	}

	public static async Task<IResult> Day([FromBody] EntryDayQuery model, IMediator mediator)
	{
		var query = new GetEntryDayListQuery
		{
			From = new DateTime(model.Year, model.Month, model.Day),
			To = new DateTime(model.Year, model.Month, model.Day)
		};

		var results = await mediator.Send(query);

		return Results.Ok(results);
	}

	public static async Task<EntryRangeModel> GetRange(DateTime from, DateTime to, IMediator mediator)
	{
		var query = new GetEntryDayListQuery
		{
			From = from.Date,
			To = to.Date
		};

		var result = await mediator.Send(query);

		return result;
	}

	public static async Task<IResult> GetWeekContainingDate(DateTime dayOfWeek, IMediator mediator) => Results.Ok(await GetRange(dayOfWeek.FirstDayOfWeek(), dayOfWeek.LastDayOfWeek(), mediator));
}

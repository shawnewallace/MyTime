using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyTime.Api.Infrastructure;
using MyTime.Api.Models;
using MyTime.App.Entries.CreateNewEntry;
using MyTime.App.Exceptions;
using MyTime.App.Infrastructure;

namespace MyTime.Api.Controllers;

public class EventStartingIntegrationEndpoints : EndpointBase, ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("eventstarting", EventStarting).WithName("eventstarting");
	}

	public static async Task<IResult> EventStarting([FromBody] AppointmentStartingNotificationModel eventDetails, IMediator mediator)
	{
		float duration = 0;

		if (eventDetails.endTime.HasValue)
		{
			TimeSpan span = eventDetails.endTime.Value - eventDetails.startTime;
			duration = (float)span.TotalHours;
		}

		var command = new CreateNewEntryCommand
		{
			OnDate = eventDetails.startTime.Date,
			Description = eventDetails.subject,
			Notes = "Created Automatically by EventStarting",
			UserId = GetCurrentUserId(),
			CorrelationId = eventDetails.eventId,
			Duration = duration.RoundToQuarter(),
			IsMeeting = true
		};

		try
		{
			var newEntry = await mediator.Send(command);

			return Results.CreatedAtRoute("get-entry", new { id = newEntry.Id });
		}
		catch (ValidationException ve)
		{
			return Results.BadRequest(ve);
		}
	}
}

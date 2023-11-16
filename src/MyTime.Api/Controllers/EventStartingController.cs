using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyTime.Api.Models;
using MyTime.App.Entries.CreateNewEntry;
using MyTime.App.Exceptions;
using MyTime.App.Infrastructure;

namespace MyTime.Api.Controllers;

public class EventStartingController : ApiControllerBase
{
	public EventStartingController(IMediator mediator) : base(mediator) { }

	[HttpPost("/eventstarting")]
	[Produces("application/json")]
	[Consumes("application/json")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
	public async Task<IActionResult> EventStarting([FromBody] AppointmentStartingNotificationModel eventDetails)
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
			var newEntry = await Mediator.Send(command);

			return CreatedAtAction(nameof(EntryController.GetEntry), "Entry", new { id = newEntry.Id }, newEntry);
		}
		catch (ValidationException ve)
		{
			return new BadRequestObjectResult(ve);
		}
	}
}

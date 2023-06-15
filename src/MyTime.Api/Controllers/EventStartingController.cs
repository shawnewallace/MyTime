using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyTime.Api.Models;
using MyTime.App.Entries.CreateNewEntry;

namespace MyTime.Api.Controllers;

public class EventStartingController : ApiControllerBase
{
	public EventStartingController(IMediator mediator) : base(mediator) { }

	[HttpPost]
	[Produces("application/json")]
	[Consumes("application/json")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
	public async Task<IActionResult> EventStarting([FromBody] AppointmentStartingNotificationModel eventDetails)
	{
		var command = new CreateNewEntryCommand
		{
			OnDate = eventDetails.startTime.Date,
			Description = eventDetails.subject,
			Notes = "Created Automatically by EventStarting",
			UserId = GetCurrentUserId(),
			CorrelationId = eventDetails.eventId
		};

		var newEntry = await Mediator.Send(command);
		return CreatedAtAction("", newEntry);
	}
}

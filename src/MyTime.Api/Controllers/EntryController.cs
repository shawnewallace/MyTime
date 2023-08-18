using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mytime.App.Entries.GetEntryList;
using MyTime.Api.Models;
using MyTime.App.Entries.CreateNewEntry;
using MyTime.App.Entries.GetEntry;
using MyTime.App.Entries.MergeEntries;
using MyTime.App.Entries.UpdateEntry;
using MyTime.App.Infrastructure;
using MyTime.App.Exceptions;
using MyTime.App.Models;

namespace MyTime.Api.Controllers;

public class EntryController : ApiControllerBase
{
	public EntryController(IMediator mediator) : base(mediator) { }

	[HttpGet]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<List<EntryModel>> List() => await Mediator.Send(new GetEntryListQuery());

	[HttpGet("/entry/{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetEntry(string id, CancellationToken ctx)
	{
		var result = await Mediator.Send(new GetEntryQuery(new Guid(id)), ctx);

		if (result is null) return NotFound();

		return Ok(result);
	}


	[HttpPost]
	[Produces("application/json")]
	[Consumes("application/json")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
	public async Task<IActionResult> Create([FromBody] NewEntryModel model)
	{
		var command = new CreateNewEntryCommand
		{
			OnDate = model.OnDate,
			Description = model.Description,
			Category = model.Category,
			Duration = model.Duration,
			IsUtilization = model.IsUtilization,
			Notes = model.Notes,
			UserId = GetCurrentUserId(),
		};

		var newEntry = await Mediator.Send(command);
		return CreatedAtAction(nameof(GetEntry), new { id = newEntry.Id }, newEntry);
	}

	[HttpPut("/entry/{id}")]
	[Produces("application/json")]
	[Consumes("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Update(string id, [FromBody] UpdateEntryModel model, CancellationToken cancellationToken)
	{
		var entryId = new Guid(id);

		var command = new UpdateEntryCommand()
		{
			Id = entryId,
			OnDate = model.OnDate,
			Description = model.Description ?? "",
			Category = model.Category ?? "",
			Duration = model.Duration,
			IsUtilization = model.IsUtilization,
			Notes = model.Notes ?? ""
		};

		try
		{
			var updatedEntry = await Mediator.Send(command, cancellationToken);

			return Ok(updatedEntry);
		}
		catch (EntryNotFoundException)
		{
			return NotFound(id);
		}
	}



	[HttpPut("/entries/merge/{primary}/{secondary}")]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
	public async Task<IActionResult> Merge(string primary, string secondary)
	{
		var primaryId = new Guid(primary);
		var secondaryId = new Guid(secondary);
		try
		{
			var mergedEntry = await Mediator.Send(new MergeEntriesCommand(primaryId, secondaryId));
			return Ok(mergedEntry);
		}
		catch (ValidationException ve)
		{
			return new UnprocessableEntityObjectResult(ve.Failures);
		}
	}
}

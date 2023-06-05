using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mytime.App.Entries.GetEntryList;
using MyTime.Api.Models;
using MyTime.App.Entries.CreateNewEntry;
using MyTime.App.Entries.MergeEntries;
using MyTime.App.Exceptions;
using MyTime.App.Models;

namespace MyTime.Api.Controllers
{

	public class EntryController : ApiControllerBase
	{
		public EntryController(IMediator mediator) : base(mediator) { }

		[HttpGet]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<List<EntryModel>> List() => await Mediator.Send(new GetEntryListQuery());

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
				Duration = model.Duration,
				IsUtilization = model.IsUtilization,
				Notes = model.Notes
			};

			var newEntry = await Mediator.Send(command);
			return Created("", newEntry);
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
}

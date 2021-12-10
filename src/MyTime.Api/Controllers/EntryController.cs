using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mytime.App.Entries.GetEntryList;
using MyTime.Api.Models;
using MyTime.App.Entries.CreateNewEntry;
using MyTime.App.EntryDays.GetEntryDayList;
using MyTime.App.Models;

namespace MyTime.Api.Controllers
{
	public class EntryDayController : ApiControllerBase
	{
		public EntryDayController() : base() { }

		[HttpGet]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<List<EntryDayModel>> List([FromBody] EntryDayQuery model)
		{
			var query = new GetEntryDayListQuery();
			query.Year = model.Year;
			query.Month = model.Month;
			query.Day = model.Day;

			return await Mediator.Send(query);
		}
	}

	public class EntryController : ApiControllerBase
	{
		public EntryController() : base() { }

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
			var command = new CreateNewEntryCommand();
			command.OnDate = model.OnDate;
			command.Description = model.Description;
			command.Duration = model.Duration;
			command.IsUtilization = model.IsUtilization;
			command.Notes = model.Notes;

			var newEntry = await Mediator.Send(command);
			return Created("", newEntry);
		}
	}
}

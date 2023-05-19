using System;
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
	}
}

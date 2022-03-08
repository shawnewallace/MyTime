using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mytime.App.Entries.GetEntryList;
using MyTime.Api.Models;
using MyTime.App.EntryDays.GetEntryDayList;
using MyTime.App.Models;

namespace MyTime.Api.Controllers
{
	public class EntryDayController : ApiControllerBase
	{
		public EntryDayController() : base() { }

		[HttpPost("day")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<List<EntryDayModel>> Day([FromBody] EntryDayQuery model)
		{
			var query = new GetEntryDayListQuery
			{
				From = new DateTime(model.Year, model.Month, model.Day)
			};

			return await Mediator.Send(query);
		}

		[HttpPost("between/{from}/{to}")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<List<EntryDayModel>> List(DateTime from, DateTime to)
		{
			var query = new GetEntryDayListQuery
			{
				From = from.Date,
				To = to.Date
			};

			var result = await Mediator.Send(query);


			return result.OrderBy(r => r.Year).ThenBy(r => r.Month).ThenBy(r => r.DayOfMonth).ToList();
		}
	}
}

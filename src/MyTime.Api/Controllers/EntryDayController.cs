using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTime.Api.Models;
using MyTime.App.EntryDays.GetEntryDayList;
using MyTime.App.Infrastructure;
using MyTime.App.Models;

namespace MyTime.Api.Controllers
{

	public class EntryDayController : ApiControllerBase
	{
		public EntryDayController() : base() { }

		[HttpPost("day")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<EntryRangeModel> Day([FromBody] EntryDayQuery model)
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
		public async Task<EntryRangeModel> GetRange(DateTime from, DateTime to)
		{
			var query = new GetEntryDayListQuery
			{
				From = from.Date,
				To = to.Date
			};

			var result = await Mediator.Send(query);

			return result;
		}

		[HttpPost("week")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<EntryRangeModel> GetWeekContainingDate(DateTime dayOfWeek)
		{
			return await GetRange(dayOfWeek.FirstDayOfWeek(), dayOfWeek.LastDayOfWeek());
		}
	}
}

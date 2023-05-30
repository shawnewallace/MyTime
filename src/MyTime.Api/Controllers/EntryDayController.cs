using System;
using System.Threading.Tasks;
using MediatR;
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
		public EntryDayController(IMediator mediator) : base(mediator) { }

		[HttpPost("/day")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<EntryRangeModel> Day([FromBody] EntryDayQuery model)
		{
			var query = new GetEntryDayListQuery
			{
				From = new DateTime(model.Year, model.Month, model.Day),
				To = new DateTime(model.Year, model.Month, model.Day)
			};

			return await Mediator.Send(query);
		}

		[HttpPost("/entries/between/{from}/{to}")]
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

		[HttpPost("/week/{dayOfWeek}")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<EntryRangeModel> GetWeekContainingDate(DateTime dayOfWeek) => await GetRange(dayOfWeek.FirstDayOfWeek(), dayOfWeek.LastDayOfWeek());
	}
}

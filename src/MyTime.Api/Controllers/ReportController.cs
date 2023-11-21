using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyTime.Api.Models;
using MyTime.App.WeekSummary;

namespace MyTime.Api.Controllers;

public class ReportController : ApiControllerBase
{
	public ReportController(IMediator mediator) : base(mediator) { }

	[HttpPost("/report/week-summary")]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<List<WeekSummaryModel>> WeekSummary([FromBody] DateRangeModel model)
		=> await Mediator.Send(request: new GetWeekSummaryQuery(model.From, model.To));
}

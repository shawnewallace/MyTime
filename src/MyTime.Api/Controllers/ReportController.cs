using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyTime.App.WeekSummary;

namespace MyTime.Api.Controllers;

public class ReportController : ApiControllerBase
{
	public ReportController(IMediator mediator) : base(mediator) { }

	[HttpGet("/report/week-summary")]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<List<WeekSummaryModel>> WeekSummary() => await Mediator.Send(request: new GetWeekSummaryQuery());
}

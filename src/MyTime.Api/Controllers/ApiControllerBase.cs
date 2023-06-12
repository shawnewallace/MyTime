using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MyTime.Api.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
	protected ApiControllerBase(IMediator mediator)
	{
		Mediator = mediator;
	}

	protected IMediator Mediator { get; }

	protected string GetCurrentUserId() => "not implemented";
}

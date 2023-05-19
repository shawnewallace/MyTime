using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MyTime.Web.Controllers;

public abstract class ControllerBase : Controller
{
	protected readonly IMediator _mediator;

	public ControllerBase(IMediator mediator) => _mediator = mediator;
}

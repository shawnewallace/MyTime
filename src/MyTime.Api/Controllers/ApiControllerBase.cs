using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace MyTime.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public abstract class ApiControllerBase : ControllerBase
	{
		private IMediator _mediator;

		protected ApiControllerBase(IMediator mediator)
		{
			_mediator = mediator;
		}

		protected IMediator Mediator => _mediator;
	}
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyTime.Api.Models;
using MyTime.App.Categories;
using MyTime.App.Categories.CreateNewCategory;
using MyTime.App.Categories.ToggleActive;
using MyTime.App.Categories.UpdateCategory;
using MyTime.App.Infrastructure;
using MyTime.App.WeekSummary;
using MyTime.Persistence.Entities;

namespace MyTime.Api.Controllers;

public class ReportController : ApiControllerBase
{
	public ReportController(IMediator mediator) : base(mediator) { }

	[HttpGet("/report/week-summary")]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<List<WeekSummaryModel>> WeekSummary() => await Mediator.Send(request: new GetWeekSummaryQuery());
}

public class CategoryController : ApiControllerBase
{
	public CategoryController(IMediator mediator) : base(mediator) { }

	[HttpGet("/categories/lookup")]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<List<CategoryNameModel>> Lookup(CancellationToken ct)
	{
		var result = await Mediator.Send(new GetActiveCategoriesListQuery(), ct);
		List<CategoryNameModel> response = new();

		foreach (var thing in result)
			response.Add(new CategoryNameModel(thing.Name));

		return response;
	}

	[HttpGet("/categories")]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<List<Category>> GetAll() => await Mediator.Send(request: new GetAllCategoriesListQuery());

	[HttpPost("/categories/between/{from}/{to}")]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<List<CategoryDayModel>> GetRange(DateTime from, DateTime to)
	{
		var query = new GetCategoryDaysQuery
		{
			From = from.Date,
			To = to.Date
		};

		var result = await Mediator.Send(query);

		return result;
	}

	[HttpPost]
	[Produces("application/json")]
	[Consumes("application/json")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
	public async Task<IActionResult> Create([FromBody] NewCategoryModel model)
	{
		var command = new CreateNewCategoryCommand
		{
			Name = model.Name,
			IsDeleted = false
		};

		var newCategory = await Mediator.Send(command);
		return Created("", newCategory);
	}

	[HttpPut("/category/{id}")]
	[Produces("application/json")]
	[Consumes("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Update(
		string id,
		[FromBody] UpdateCategoryModel model,
		CancellationToken cancellationToken)
	{
		var categoryId = new Guid(id);

		var command = new UpdateCategoryCommand()
		{
			Id = categoryId,
			Name = model.Name
		};

		try
		{
			var updatedEntry = await Mediator.Send(command, cancellationToken);

			return Ok(updatedEntry);
		}
		catch (CategoryNotFoundException)
		{
			return NotFound(id);
		}
	}

	[HttpPut("/category/{id}/toggle-active")]
	[Produces("application/json")]
	[Consumes("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> ToggleActive(
		string id,
		CancellationToken cancellationToken)
	{
		var categoryId = new Guid(id);

		var command = new ToggleActiveCommand()
		{
			Id = categoryId,
		};

		try
		{
			await Mediator.Send(command, cancellationToken);

			return Ok();
		}
		catch (CategoryNotFoundException)
		{
			return NotFound(id);
		}
	}
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyTime.Api.Models;
using MyTime.App.Categories;
using MyTime.App.Categories.ToggleActive;
using MyTime.App.Categories.UpdateCategory;
using MyTime.App.Infrastructure;
using MyTime.Persistence.Entities;

namespace MyTime.Api.Controllers;

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

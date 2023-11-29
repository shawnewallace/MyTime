using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyTime.Api.Infrastructure;
using MyTime.App.Categories;
using MyTime.App.Categories.CreateNewCategory;
using MyTime.App.Categories.ToggleActive;
using MyTime.App.Categories.UpdateCategory;
using MyTime.App.Infrastructure;
using MyTime.Persistence.Entities;

namespace MyTime.Api.Categories;

public class CategoryEndpoints : EndpointBase, ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("categories").WithName("refactor-category-endpoints");
		group.MapGet("lookup", Lookup).WithName("lookup");
		group.MapGet("", GetAll).WithName("get-all-categories");
		group.MapPost("between/{from}/{to}", GetRange).WithName("get-range-of-categories");
		group.MapPost("", Create).WithName("create-category");
		group.MapPut("{id}", Update).WithName("update-category");
		group.MapPut("{id}/toggle-active", ToggleActive).WithName("toggle-category-active");
	}

	public static async Task<IResult> Lookup(IMediator mediator)
	{
		var result = await mediator.Send(new GetActiveCategoriesListQuery());
		List<CategoryNameModel> response = new();

		foreach (var thing in result)
			response.Add(new CategoryNameModel(thing.Name));

		return Results.Ok(response);
	}

	public static async Task<IResult> GetAll(IMediator mediator) => Results.Ok(await mediator.Send(request: new GetAllCategoriesListQuery()));

	public async Task<IResult> GetRange(DateTime from, DateTime to, IMediator mediator)
	{
		var query = new GetCategoryDaysQuery
		{
			From = from.Date,
			To = to.Date
		};

		var result = await mediator.Send(query);

		return Results.Ok(result);
	}

	public static async Task<IResult> Create([FromBody] NewCategoryModel model, IMediator mediator)
	{
		var command = new CreateNewCategoryCommand
		{
			Name = model.Name,
			IsDeleted = false
		};

		var newCategory = await mediator.Send(command);
		return Results.Created("", newCategory);
	}

	public static async Task<Results<Ok<Category>, NotFound<string>>> Update(
		string id,
		[FromBody] UpdateCategoryModel model,
		CancellationToken cancellationToken,
		IMediator mediator)
	{
		var categoryId = new Guid(id);

		var command = new UpdateCategoryCommand()
		{
			Id = categoryId,
			Name = model.Name
		};

		try
		{
			var updatedEntry = await mediator.Send(command, cancellationToken);

			return TypedResults.Ok(updatedEntry);
		}
		catch (CategoryNotFoundException)
		{
			return TypedResults.NotFound(id);
		}
	}

	public static async Task<IResult> ToggleActive(
		string id,
		CancellationToken cancellationToken,
		IMediator mediator)
	{
		var categoryId = new Guid(id);

		var command = new ToggleActiveCommand()
		{
			Id = categoryId,
		};

		try
		{
			await mediator.Send(command, cancellationToken);

			return Results.Ok();
		}
		catch (CategoryNotFoundException)
		{
			return Results.NotFound(id);
		}
	}
}

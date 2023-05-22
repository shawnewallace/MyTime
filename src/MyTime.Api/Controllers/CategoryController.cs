using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTime.Api.Models;
using MyTime.App.Categories;
using MyTime.Persistence.Entities;

namespace MyTime.Api.Controllers
{
	public class CategoryController : ApiControllerBase
	{
		public CategoryController() : base() { }

		[HttpGet("/categories/lookup")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<List<CategoryNameModel>> Lookup()
		{
			var result = await Mediator.Send(new GetActiveCategoriesListQuery());
			List<CategoryNameModel> response = new();

			foreach (var thing in result)
				response.Add(new CategoryNameModel(thing.Name));

			return response;
		}

		[HttpGet("/categories")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<List<Category>> GetAll() => await Mediator.Send(new GetActiveCategoriesListQuery());
	}
}

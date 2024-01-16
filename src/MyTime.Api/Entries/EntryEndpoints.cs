using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mytime.App.Entries.GetEntryList;
using MyTime.Api.Infrastructure;
using MyTime.Api.Models;
using MyTime.App.Entries.CreateNewEntry;
using MyTime.App.Entries.DeleteEntry;
using MyTime.App.Entries.GetEntry;
using MyTime.App.Entries.MergeEntries;
using MyTime.App.Entries.UpdateEntry;
using MyTime.App.Exceptions;
using MyTime.App.Infrastructure;

namespace MyTime.Api.Entries;

public class EntryEndpoints : EndpointBase, ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    RouteGroupBuilder group = app.MapGroup("entries").WithName("entry-endpoints");
    group.MapGet("", List).WithName("list-entries");
    group.MapGet("{id}", GetEntry).WithName("get-entry");
    group.MapPost("", Create).WithName("create-entry");
    group.MapPut("{id}", Update).WithName("update-entry");
    group.MapPut("merge/{primary}/{secondary}", Merge).WithName("merge-entries");
    group.MapDelete("{id}", Delete).WithName("delete-entry");
  }

  public static async Task<IResult> List(IMediator mediator) => Results.Ok(await mediator.Send(new GetEntryListQuery()));

  public static async Task<IResult> GetEntry(string id, IMediator mediator)
  {
    App.Entries.EntryModel? result = await mediator.Send(new GetEntryQuery(new Guid(id)));

    if (result is null)
    {
      return Results.NotFound();
    }

    return Results.Ok(result);
  }

  public static async Task<IResult> Create([FromBody] NewEntryModel model, IMediator mediator)
  {
    var command = new CreateNewEntryCommand
    {
      OnDate = model.OnDate,
      Description = model.Description,
      CategoryId = string.IsNullOrEmpty(model.CategoryId) ? null : new Guid(model.CategoryId),
      Duration = model.Duration,
      IsUtilization = model.IsUtilization ?? false,
      Notes = model.Notes,
      UserId = GetCurrentUserId(),
    };

    App.Entries.EntryModel newEntry = await mediator.Send(command);
    return Results.Created("", newEntry);
  }

  public static async Task<IResult> Update(
    string id,
    [FromBody] UpdateEntryModel model,
    CancellationToken cancellationToken,
    IMediator mediator)
  {
    var entryId = new Guid(id);

    var command = new UpdateEntryCommand()
    {
      Id = entryId,
      OnDate = model.OnDate
    };

    if (model.Description is not null)
    {
      command.Description = model.Description;
    }
    // if (model.Category is not null) command.Category = model.Category;
    if (!string.IsNullOrEmpty(model.CategoryId))
    {
      command.CategoryId = new Guid(model.CategoryId);
    }

    if (model.Duration > 0)
    {
      command.Duration = model.Duration;
    }

    if (model.IsUtilization is not null)
    {
      command.IsUtilization = model.IsUtilization;
    }

    if (model.Notes is not null)
    {
      command.Notes = model.Notes;
    }

    try
    {
      App.Entries.EntryModel updatedEntry = await mediator.Send(command, cancellationToken);

      return Results.Ok(updatedEntry);
    }
    catch (EntryNotFoundException)
    {
      return Results.NotFound(id);
    }
  }

  public static async Task<IResult> Merge(string primary, string secondary, IMediator mediator)
  {
    var primaryId = new Guid(primary);
    var secondaryId = new Guid(secondary);
    try
    {
      App.Entries.EntryModel mergedEntry = await mediator.Send(new MergeEntriesCommand(primaryId, secondaryId));
      return Results.Ok(mergedEntry);
    }
    catch (ValidationException ve)
    {
      return Results.UnprocessableEntity(ve.Failures);
    }
  }

  public static async Task<IResult> Delete(string id, IMediator mediator)
  {
    var entryId = new Guid(id);
    try
    {
      await mediator.Send(new DeleteEntryCommand(entryId, GetCurrentUserId()));
      return Results.NoContent();
    }
    catch (EntryNotFoundException)
    {
      return Results.NotFound(id);
    }
  }
}

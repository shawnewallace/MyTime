using System;
using MediatR;

namespace MyTime.App.Entries.DeleteEntry;
public class DeleteEntryCommand : IRequest
{
  public Guid Id { get; set; }
  public string UserId { get; set; }

  public DeleteEntryCommand() { }
  public DeleteEntryCommand(Guid id, string userId)
  {
    Id = id;
    UserId = userId;
  }
}

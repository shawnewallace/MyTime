using System;
using System.Threading;
using MyTime.App.Entries.MergeEntries;
using MyTime.App.Exceptions;
using MyTime.Integration.Tests.Infrastructure;
using MyTime.Persistence;
using MyTime.Persistence.Entities;
using Shouldly;
using Xunit;

namespace MyTime.Integration.Tests;

public class MergeEntriesCommandHandlerTests : DbTestBase
{
	private readonly MyTimeSqlDbContext _context;
	private readonly MergeEntriesCommandHandler _commandHandler;
	private MergeEntriesCommand _command;

	public MergeEntriesCommandHandlerTests()
	{
		_context = GetDbContext(false);
		_commandHandler = new MergeEntriesCommandHandler(_context);
	}

	[Fact]
	public async void ValidationExceptionWhenPrimaryDoesNotExist()
	{
		_command = new MergeEntriesCommand(Guid.NewGuid(), Guid.NewGuid());
		await Should.ThrowAsync<ValidationException>(() => _commandHandler.Handle(_command, CancellationToken.None));
	}

	[Fact]
	public async void ValidationExceptionWhenPrimaryHasBeenDeleted()
	{
		var p = GenerateEvent();
		_context.Entries.Add(p);
		await _context.SaveChangesAsync();

		_command = new MergeEntriesCommand(p.Id, Guid.NewGuid());
		await Should.ThrowAsync<ValidationException>(() => _commandHandler.Handle(_command, CancellationToken.None));
	}

	[Fact]
	public async void ValidationExceptionWhenSecondaryDoesNotExist()
	{
		var p = GenerateEvent();
		_context.Entries.Add(p);
		await _context.SaveChangesAsync();

		var id = Guid.NewGuid();
		_command = new MergeEntriesCommand(id, Guid.NewGuid());
		await Should.ThrowAsync<ValidationException>(() => _commandHandler.Handle(_command, CancellationToken.None));
	}

	[Fact]
	public async void ValidationExceptionWhenSecondaryHasBeenDeleted()
	{
		var p = GenerateEvent();
		var s = GenerateEvent(isDeleted: true);

		_context.Entries.Add(p);
		_context.Entries.Add(s);
		await _context.SaveChangesAsync();

		_command = new MergeEntriesCommand(p.Id, s.Id);
		await Should.ThrowAsync<ValidationException>(() => _commandHandler.Handle(_command, CancellationToken.None));
	}

	[Fact]
	public async void ValidationExceptionWhenDatesAreNotTheSame()
	{
		var p = GenerateEvent();
		p.OnDate = new DateTime(2021, 10, 22);
		var s = GenerateEvent();

		_context.Entries.Add(p);
		_context.Entries.Add(s);
		await _context.SaveChangesAsync();

		_command = new MergeEntriesCommand(p.Id, s.Id);
		await Should.ThrowAsync<ValidationException>(() => _commandHandler.Handle(_command, CancellationToken.None));
	}

	[Fact]
	public async void DurationShouldBeTheSumOfBoth()
	{
		var p = GenerateEvent(duration: 1.25f);
		var s = GenerateEvent(duration: 2.5f);

		_context.Entries.Add(p);
		_context.Entries.Add(s);
		await _context.SaveChangesAsync();

		_command = new MergeEntriesCommand(p.Id, s.Id);

		var result = await _commandHandler.Handle(_command, CancellationToken.None);

		result.Duration.ShouldBe(3.75f);
	}

	[Fact]
	public async void IfPrimaryIsBillableWillBeBillable(){
		var p = GenerateEvent();
		var s = GenerateEvent();

		p.IsUtilization = true;

		_context.Entries.Add(p);
		_context.Entries.Add(s);
		await _context.SaveChangesAsync();

		_command = new MergeEntriesCommand(p.Id, s.Id);

		var result = await _commandHandler.Handle(_command, CancellationToken.None);

		result.IsUtilization.ShouldBeTrue();
	}

	[Fact]
	public async void IfSecondaryIsBillableWillBeBillable()
	{
		var p = GenerateEvent();
		var s = GenerateEvent();

		s.IsUtilization = true;

		_context.Entries.Add(p);
		_context.Entries.Add(s);
		await _context.SaveChangesAsync();

		_command = new MergeEntriesCommand(p.Id, s.Id);

		var result = await _commandHandler.Handle(_command, CancellationToken.None);

		result.IsUtilization.ShouldBeTrue();
	}

	[Fact]
	public async void SecondaryIsDeleted()
	{
		var p = GenerateEvent();
		var s = GenerateEvent();

		s.IsUtilization = true;

		_context.Entries.Add(p);
		_context.Entries.Add(s);
		await _context.SaveChangesAsync();

		_command = new MergeEntriesCommand(p.Id, s.Id);

		var result = await _commandHandler.Handle(_command, CancellationToken.None);

		s.IsDeleted.ShouldBeTrue();
	}

	private Entry GenerateEvent(float duration = 1.25f, bool isDeleted = false)
	{
		return new Entry
		{
			Id = Guid.NewGuid(),
			OnDate = new DateTime(2021, 10, 21),
			Description = $"Description - {Guid.NewGuid().ToString()}",
			Duration = duration,
			IsDeleted = isDeleted
		};
	}
}
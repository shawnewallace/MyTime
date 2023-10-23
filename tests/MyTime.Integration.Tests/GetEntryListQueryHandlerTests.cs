using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MyTime.App.Entries;
using Mytime.App.Entries.GetEntryList;
using MyTime.Integration.Tests.Infrastructure;
using MyTime.Persistence;
using Shouldly;
using Xunit;

namespace MyTime.Integration.Tests;
[Collection("QueryCollection")]
public class GetEntryListQueryHandlerTests
{
	private MyTimeSqlDbContext _context;
	private readonly GetEntryListQueryHandler _handler;

	public GetEntryListQueryHandlerTests(QueryTestFixture fixture)
	{
		_context = fixture.Context;
		_handler = new GetEntryListQueryHandler(_context);
	}

	[Fact]
	public async Task ReturnsTheCorrectType()
	{
		var result = await _handler.Handle(new GetEntryListQuery(), CancellationToken.None);
		result.ShouldBeOfType<List<EntryModel>>();
	}

	[Fact]
	public async Task ResultShouldNotBeEmpty()
	{
		var result = await _handler.Handle(new GetEntryListQuery(), CancellationToken.None);
		result.Count.ShouldBeGreaterThanOrEqualTo(3);
	}
}

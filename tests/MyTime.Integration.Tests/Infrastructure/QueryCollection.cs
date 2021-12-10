using Xunit;

namespace MyTime.Integration.Tests.Infrastructure
{

	[CollectionDefinition("QueryCollection")]
	public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
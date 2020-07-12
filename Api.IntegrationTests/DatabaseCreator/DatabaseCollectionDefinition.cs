using Xunit;

namespace Api.IntegrationTests.DatabaseCreator
{
    [CollectionDefinition("DatabaseSetup")]
    public class DatabaseCollectionDefinition : ICollectionFixture<DatabaseFixture>
    {
    }
}

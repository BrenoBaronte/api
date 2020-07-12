
using Api.Repositories.DbConnection;
using AutoFixture;

namespace Api.IntegrationTests.AutoData
{
    public class SqlConnectionCustomization : ICustomization
    {
        private const string ConnectionString =
            "Data Source=(localdb)\\mssqllocaldb; Initial Catalog=ApiDatabaseTest; Trusted_Connection=True; MultipleActiveResultSets=true";

        public void Customize(IFixture fixture)
        {
            fixture.Register(() => new SqlConnectionFactory(ConnectionString));
        }
    }
}

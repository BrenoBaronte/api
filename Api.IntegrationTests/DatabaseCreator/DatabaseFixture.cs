namespace Api.IntegrationTests.DatabaseCreator
{
    public class DatabaseFixture
    {
        public DatabaseFixture()
        {
            var connectionString = SqlServerServices.GetConnectionString();
            SqlServerServices.CreateDatabase("ApiDatabaseTest", connectionString);
            SqlServerServices.CreateTestData(connectionString);
        }
    }
}

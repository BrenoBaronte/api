using Api.Repositories.DbConnection.Interfaces;
using System.Data.SqlClient;

namespace Api.Repositories.DbConnection
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        public string DatabaseConnectionString { get; }

        public SqlConnectionFactory(
            string databaseConnectionString)
        {
            DatabaseConnectionString = databaseConnectionString
                ?? throw new System.ArgumentNullException(nameof(databaseConnectionString));
        }

        public SqlConnection GetConnection()
            => new SqlConnection(DatabaseConnectionString);
    }
}

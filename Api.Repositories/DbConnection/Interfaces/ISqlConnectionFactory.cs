using System.Data.SqlClient;

namespace Api.Repositories.DbConnection.Interfaces
{
    public interface ISqlConnectionFactory
    {
        SqlConnection GetConnection();
    }
}

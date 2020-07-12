using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Api.IntegrationTests.DatabaseCreator
{
    public class SqlServerServices
    {
        public static string GetConnectionString()
        {
            return "Data Source=(localdb)\\mssqllocaldb; Initial Catalog=Master; Trusted_Connection=True; MultipleActiveResultSets=true";
        }

        public static void CreateDatabase(
            string databaseName,
            string connectionString)
        {
            CreateDatabase(databaseName, connectionString, "Create_Objects.sql");
        }

        public static void CreateTestData(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                ExecuteScriptFromAssembly("Test_Data.sql", connection);
            }
        }

        private static void ExecuteScriptFromAssembly(string scriptName, SqlConnection connection)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(
                $"Api.IntegrationTests.DatabaseCreator.Scripts.{scriptName}");
            var textStreamReader = new StreamReader(stream);
            ExecuteNonQuery(connection, textStreamReader.ReadToEnd());
        }

        private static void CreateDatabase(string databaseName, string connectionString, string schemaScriptFile)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                ExecuteNonQuery(connection,
                    string.Format(
                        $"IF EXISTS(select * from sys.databases where name='{databaseName}') BEGIN ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE DROP DATABASE [{databaseName}] END"));
                ExecuteNonQuery(connection, "CREATE DATABASE " + databaseName);
            }

            using (var connection = new SqlConnection(connectionString.ToLower().Replace("master", databaseName)))
            {
                ExecuteScriptFromAssembly(schemaScriptFile, connection);
            }
        }

        public static void ExecuteNonQuery(SqlConnection connection, string commandText)
        {
            try
            {
                connection.Open();

                var command = connection.CreateCommand();

                var commandStrings = Regex.Split(commandText, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                foreach (var script in commandStrings)
                {
                    command.CommandText = script;
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                if (connection.State != ConnectionState.Closed) connection.Close();
            }
        }
    }
}

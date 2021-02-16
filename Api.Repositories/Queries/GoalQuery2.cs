using Api.Domain.Entities;
using Api.Domain.Queries;
using Api.Repositories.DbConnection.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Api.Repositories.Queries
{
    /// <summary>
    /// Implementation of IGoalQuery using Ado.Net
    /// </summary>
    public class GoalQuery2 : IGoalQuery
    {
        public ISqlConnectionFactory SqlConnectionFactory { get; }

        public GoalQuery2(ISqlConnectionFactory sqlConnectionFactory)
        {
            SqlConnectionFactory = sqlConnectionFactory
                ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));
        }

        public async Task<List<Goal>> GetAllAsync()
        {
            using (var connection = SqlConnectionFactory.GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText =
                    "SELECT * FROM [Goal] (NOLOCK)";

                var dataReader = await command.ExecuteReaderAsync();

                return MapList(dataReader);
            }
        }

        public async Task<Goal> GetAsync(Guid goalId)
        {
            using (var connection = SqlConnectionFactory.GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText =
                    "SELECT * FROM [Goal] (NOLOCK) WHERE Id=@goalId";

                command.Parameters.AddWithValue("goalId", goalId);

                var dataReader = await command.ExecuteReaderAsync();

                return dataReader.Read() ? MapGoal(dataReader) : null;
            }
        }

        private List<Goal> MapList(SqlDataReader dataReader)
        {
            var result = new List<Goal>();

            while (dataReader.Read())
            {
                result.Add(MapGoal(dataReader));
            }

            return result;
        }

        private Goal MapGoal(SqlDataReader dataReader)
        {
            return new Goal
            {
                Id = (Guid)dataReader["Id"],
                Title = (string)dataReader["Title"],
                Count = (int)dataReader["Count"]
            };
        }
    }
}

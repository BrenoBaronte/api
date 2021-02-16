using Api.Domain.Entities;
using Api.Domain.Repositories;
using Api.Repositories.DbConnection.Interfaces;
using System;
using System.Threading.Tasks;

namespace Api.Repositories.Commands
{
    /// <summary>
    /// Implementation of IGoalCommand using Ado.Net
    /// </summary>
    public class GoalCommand2 : IGoalCommand
    {
        public ISqlConnectionFactory SqlConnectionFactory { get; }

        public GoalCommand2(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            SqlConnectionFactory = sqlConnectionFactory
                ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));
        }

        public async Task<bool> CreateAsync(Goal goal)
        {
            using (var connection = SqlConnectionFactory.GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = "INSERT INTO [Goal] (Id, Title, Count) " +
                    "VALUES (@Id, @Title, @Count)";

                command.Parameters.AddWithValue("Id", goal.Id);
                command.Parameters.AddWithValue("Title", goal.Title);
                command.Parameters.AddWithValue("Count", goal.Count);

                var rollsAffected = await command.ExecuteNonQueryAsync();

                return rollsAffected == 1;
            }
        }

        public async Task<bool> UpdateAsync(Goal goal)
        {
            using (var connection = SqlConnectionFactory.GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText =
                    "UPDATE [Goal] SET Title = @Title, Count = @Count " +
                    "WHERE Id = @Id";

                command.Parameters.AddWithValue("Id", goal.Id);
                command.Parameters.AddWithValue("Title", goal.Title);
                command.Parameters.AddWithValue("Count", goal.Count);

                var rollsAffected = await command.ExecuteNonQueryAsync();

                return rollsAffected == 1;
            }
        }

        public async Task<bool> DeleteAsync(Guid goalId)
        {
            using (var connection = SqlConnectionFactory.GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText =
                    "DELETE FROM [Goal] WHERE Id = @Id";

                command.Parameters.AddWithValue("Id", goalId);

                var rollsAffected = await command.ExecuteNonQueryAsync();

                return rollsAffected == 1;
            }
        }
    }
}

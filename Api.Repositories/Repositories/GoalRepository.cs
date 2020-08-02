using Api.Domain.Entities;
using Api.Domain.Repositories;
using Api.Repositories.DbConnection.Interfaces;
using Dapper;
using System;
using System.Threading.Tasks;

namespace Api.Repositories.Repositories
{
    public class GoalRepository : IGoalRepository
    {
        public ISqlConnectionFactory SqlConnectionFactory { get; }

        public GoalRepository(
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

                var rollsAffeected = await connection.ExecuteAsync(
                    "INSERT INTO [Goal] (Id, Title, Count) " +
                    "VALUES (@Id, @Title, @Count)", goal);

                return rollsAffeected == 1;
            }
        }

        public async Task<bool> UpdateAsync(Goal goal)
        {
            using (var connection = SqlConnectionFactory.GetConnection())
            {
                connection.Open();

                var rollsAffected = await connection.ExecuteAsync(
                    "UPDATE [Goal] SET Title = @Title, Count = @Count " +
                    "WHERE Id = @Id", goal);

                return rollsAffected == 1;
            }
        }

        public async Task<bool> DeleteAsync(Guid goalId)
        {
            using (var connection = SqlConnectionFactory.GetConnection())
            {
                connection.Open();

                var rollsAffected = await connection.ExecuteAsync(
                    "DELETE FROM [Goal] WHERE Id = @Id",
                    new { Id = goalId });

                return rollsAffected == 1;
            }
        }
    }
}

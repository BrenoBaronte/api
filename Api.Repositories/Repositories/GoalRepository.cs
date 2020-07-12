using Api.Domain.Entities;
using Api.Domain.Repositories;
using Api.Repositories.DbConnection.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Linq;
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
                ?? throw new System.ArgumentNullException(nameof(sqlConnectionFactory));
        }

        public async Task<List<Goal>> GetAllAsync()
        {
            using (var connection = SqlConnectionFactory.GetConnection())
            {
                connection.Open();

                var goals = await connection.QueryAsync<Goal>(
                    "SELECT * FROM [Goal] (NOLOCK)");

                return goals.ToList();
            }
        }
    }
}

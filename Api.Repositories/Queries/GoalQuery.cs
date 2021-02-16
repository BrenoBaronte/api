using Api.Domain.Entities;
using Api.Domain.Queries;
using Api.Repositories.DbConnection.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories.Queries
{
    /// <summary>
    /// Implementation of IGoalQuery using Dapper
    /// </summary>
    public class GoalQuery : IGoalQuery
    {
        public ISqlConnectionFactory SqlConnectionFactory { get; }

        public GoalQuery(ISqlConnectionFactory sqlConnectionFactory)
        {
            SqlConnectionFactory = sqlConnectionFactory
                ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));
        }

        public async Task<Goal> GetAsync(Guid goalId)
        {
            using (var connection = SqlConnectionFactory.GetConnection())
            {
                connection.Open();

                var goal = (await connection.QueryAsync<Goal>(
                    "SELECT * FROM [Goal] (NOLOCK) WHERE Id=@goalId",
                    new { goalId }))
                    .FirstOrDefault();

                return goal;
            }
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

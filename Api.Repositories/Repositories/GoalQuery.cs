using Api.Domain.Entities;
using Api.Domain.Repositories;
using Api.Repositories.DbConnection.Interfaces;
using Dapper;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories.Repositories
{
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
    }
}
